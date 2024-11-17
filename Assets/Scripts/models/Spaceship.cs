using System;
using DefaultNamespace;
using DefaultNamespace.models;
using Unity.Mathematics.Geometry;
using UnityEngine;
using Math = Unity.Mathematics.Geometry.Math;

/**
 * This is a class that contains all the logic for moving objects in the scene.
 * It will contain all properties (position velocity, acceleration, etc.) and methods (updating position, etc.)
 * for moving objects.
 */
public class Spaceship : Entity
{
    // Velocity of the object
    public double vx, vy, vz;
    // Acceleration of the object
    public double ax, ay, az;
    
    // Gravitational acceleration that the object is experiencing (from the blackhole, centripital).
    public double g;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the position of the object by reading the transform component of the object
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        Vector3 oPos = new Vector3((float) x, (float) y, (float) z);
        
        // Calculate the initial velocity of the object
        vx = CalculateTangentialShipVelocity(oPos);
        vy = 0.0f;
        vz = 0.0f;
        
        // Calculate the initial acceleration of the object
        g = CalculateCentripitalAcceleration(oPos);
    }

    // Update is called once per frame
    void Update()
    {
        double deltaTime = Time.deltaTime;
        
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        
        // As the object moves in circle, we need to consider the centrifulgal force
        // We calculate the acceleration of the object by using the formula a = v^2 / r
        // r will be from the center of the blackhole (0, 0, 0)
        double r = new Vector3((float) x, (float) y, (float) z).magnitude;
        
        // Calculate the acceleration of the object in each axis using trigonometry
        // Angle between the direction vector and the x-axis
        float angleX = Mathf.Acos((float) (x / r));
        // Angle between the direction vector and the y-axis
        float angleY = 0.0f;    // We keep this at 0 because we don't want to move in the y-axis.
        // Angle between the direction vector and the z-axis
        float angleZ = Mathf.Acos((float) (z / r));
        
        ax = g * Mathf.Cos(angleX);
        ay = g * Mathf.Cos(angleY);
        az = g * Mathf.Cos(angleZ);
        
        // Update the velocity of the object by adding the acceleration to the velocity TO-DO
        vx += ax * deltaTime; // TO-CHECK
        vy += ay * deltaTime;
        vz += az * deltaTime;
        
        // Update the position of the object by adding the velocity to the position TO-DO
        x += vx * deltaTime; // TO-CHECK
        y += vy * deltaTime;
        z += vz * deltaTime;
        
        // Update the transform component of the object with the new position
        transform.position = new Vector3((float) x, (float) y, (float) z);
    }

    public double SmoothStep(double x, double threshold)
    {
        double STEEPNESS = 1.0f;
        return 1.0f / (1.0f + Mathf.Exp((float) (-(x - threshold) * STEEPNESS)));
    }
    
    // To-DO
    public Vector3 LorentzVelocityTransformation(Vector3 sourceVelocity, Vector3 observerVelocity)
    {
        float v = Vector3.Magnitude(observerVelocity);
        
        if (v > 0.0)
        {
            Vector3 velocityAxis = -observerVelocity / v;
            float gamma = 1.0f / Mathf.Sqrt(1.0f - v * v);
            
            float movingParameter = Vector3.Dot(sourceVelocity, velocityAxis);
            Vector3 movingPerpendicular = sourceVelocity - velocityAxis * movingParameter;

            float denom = 1.0f + v * movingParameter;
            return (velocityAxis * (movingParameter + v) + movingPerpendicular / gamma) / denom;

        }
        
        
        return sourceVelocity;
    }

    public float Contract(Vector3 position, Vector3 direction, float mult)
    {
        float par = Vector3.Dot(position, direction);
        return (position - par * direction + direction * par * mult).magnitude;
    }

    public Vector3 RayTraceLogic(Vector3 cameraPos, Vector3 direction, double stepSize, int maxSteps)
    {
        double r = cameraPos.magnitude;
        double u = 1 / r;
        double du = -u * Vector3.Dot(direction.normalized, cameraPos.normalized);

        for (int step = 0; step < maxSteps; step++)
        {
            double duNext = du - u * stepSize * (1-3*u*u);
            double uNext = u + stepSize * du;

            if (uNext < 0) break;
            du = duNext;
            u = uNext;
            Vector3 pos = cameraPos + direction.normalized * (float) u;
            // Debug.Log("pos: " + pos + " du: " + du + " u: " + u);
            
        }

        Vector3 planeNormal = cameraPos.normalized;
        Vector3 tangent = Vector3.Cross(Vector3.Cross(planeNormal, direction), planeNormal).normalized;
        double phi = stepSize * maxSteps;
        Vector3 position = (planeNormal * Mathf.Cos((float) phi) + tangent * Mathf.Sin((float) phi)) / (float) u;
        return position;
    }

    public double CalculateTimeCurvature(Vector3 oPos)
    {
        double R = oPos.magnitude;
        int c = Constants.v_light; // speed of light
        double Rs = Constants.RS;
        
        double gTT = (1-(Rs/R) * Mathf.Pow(c, 2));
        
        return gTT;
    }
    
    public double CalculateSpaceCurvature(Vector3 oPos)
    {
        double R = oPos.magnitude;
        
        double gRR = 1/(1-(Constants.RS/R));
        return gRR;
    }

    public double CalculateGravitationalLensing(Vector3 oPos)
    {
        return 2*Constants.RS/oPos.magnitude; //radians
    }
    
    public double TimeDilationByShip(Vector3 oPos) 
    {
        return Mathf.Sqrt((float) CalculateTimeCurvature(oPos)); // % of which time goes slower/faster than light
    }

    public double CalculateGravitationalRedshift(Vector3 oPos)
    {
        return 1 / Mathf.Sqrt((float) CalculateTimeCurvature(oPos));
    }

    public double SpaceDilationByShip(Vector3 oPos)
    {
        // TO-DO
        return 1;
    }

    public double CalculateLorentzTime(double time, Vector3 MovingVelocity)
    {
        double relativeTime = time / Mathf.Sqrt(1-(Mathf.Pow(MovingVelocity.magnitude, 2)/Mathf.Pow(Constants.v_light, 2)));
        return relativeTime;
    }
    
    public float CalculateTangentialShipVelocity(Vector3 oPos)
    {
        Debug.Log("rs: " + Constants.RS_Unity);
        Debug.Log("r: " + oPos.magnitude);
        Debug.Log("Constants.G_Unity: " + Constants.G_Unity);
        Debug.Log("Constants.M_Unity: " + Constants.M_Unity);
        Debug.Log("(1 - Constants.RS_Unity / oPos.magnitude): " + (1 - Constants.RS_Unity / oPos.magnitude));
        double f = Constants.G_Unity * Constants.M_Unity / oPos.magnitude * (1 - Constants.RS_Unity / oPos.magnitude);
        Debug.Log("f: " + f);
        return Mathf.Sqrt((float) f);
    }

    public double CalculateCentripitalAcceleration(Vector3 oPos)
    {
        return Mathf.Pow(CalculateTangentialShipVelocity(oPos), 2);
    }

    public double CalculateCriticalNetRelavisticAcceleration(Vector3 oPos)
    {
        return ((Constants.G * Constants.M / Mathf.Pow(oPos.magnitude, 2) * Mathf.Pow((1 - (float) (Constants.RS / oPos.magnitude)), 0.5f)));
    }
    
    // Relativity makes sure that total acceleration is less than critical
    public bool RelativityCheck(Vector3 oPos)
    {
        if (CalculateCentripitalAcceleration(oPos) > CalculateCriticalNetRelavisticAcceleration(oPos))
        {
            return true;
        }
        return false;
    }
}
