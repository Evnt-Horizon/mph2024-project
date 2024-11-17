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
    public float vx, vy, vz;
    // Acceleration of the object
    public float ax, ay, az;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the position of the object by reading the transform component of the object
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        
        // Set the transform component of the object with the new position
        transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        // As the object moves in circle, we need to consider the centrifulgal force
        // We calculate the acceleration of the object by using the formula a = v^2 / r
        // r will be from the center of the blackhole (0, 0, 0)
        float r = Mathf.Sqrt(
            Mathf.Pow((float)x, 2) +
            Mathf.Pow((float)y, 2) +
            Mathf.Pow((float)z, 2)
        );
        
        
        // Update the velocity of the object by adding the acceleration to the velocity TO-DO
        vx += ax; // To-Check
        vy += ay;
        vz += az;
        
        // Update the position of the object by adding the velocity to the position TO-DO
        x += vx;
        y += vy;
        z += vz;
        
        // Update the transform component of the object with the new position
        transform.position = new Vector3(x, y, z);
    }
    
    public void SetAcceleration(float ax, float ay, float az)
    {
        this.ax = ax;
        this.ay = ay;
        this.az = az;
    }

    public float SmoothStep(float x, float threshold)
    {
        float STEEPNESS = 1.0f;
        return 1.0f / (1.0f + Mathf.Exp(-(x - threshold) * STEEPNESS));
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

    public Vector3 RayTraceLogic(Vector3 cameraPos, Vector3 direction, float stepSize, int maxSteps)
    {
        float r = cameraPos.magnitude;
        float u = 1 / r;
        float du = -u * Vector3.Dot(direction.normalized, cameraPos.normalized);

        for (int step = 0; step < maxSteps; step++)
        {
            float duNext = du - u * stepSize * (1-3*u*u);
            float uNext = u + stepSize * du;

            if (uNext < 0) break;
            du = duNext;
            u = uNext;
            Vector3 pos = cameraPos + direction.normalized * u;
            // Debug.Log("pos: " + pos + " du: " + du + " u: " + u);
            
        }

        Vector3 planeNormal = cameraPos.normalized;
        Vector3 tangent = Vector3.Cross(Vector3.Cross(planeNormal, direction), planeNormal).normalized;
        float phi = stepSize * maxSteps;
        Vector3 position = (planeNormal * Mathf.Cos(phi) + tangent * Mathf.Sin(phi)) / u;
        return position;
    }

    public float CalculateTimeCurvature(Vector3 oPos)
    {
        float R = oPos.magnitude;
        int c = Constants.v_light; // speed of light
        float Rs = Constants.RS;
        
        float gTT = (1-(Rs/R) * Mathf.Pow(c, 2));
        
        return gTT;
    }
    
    public float CalculateSpaceCurvature(Vector3 oPos)
    {
        float R = oPos.magnitude;
        
        float gRR = 1/(1-(Constants.RS/R));
        return gRR;
    }

    public float CalculateGravitationalLensing(Vector3 oPos)
    {
        return 2*Constants.RS/oPos.magnitude; //radians
    }
    
    public float TimeDilationByShip(Vector3 oPos) 
    {
        return Mathf.Sqrt(CalculateTimeCurvature(oPos)); // % of which time goes slower/faster than light
    }

    public float CalculateGravitationalRedshift(Vector3 oPos)
    {
        return 1 / Mathf.Sqrt(CalculateTimeCurvature(oPos));
    }

    public float SpaceDilationByShip(Vector3 oPos)
    {
        // TO-DO
        return 1;
    }

    public float CalculateLorentzTime(float time, Vector3 MovingVelocity)
    {
        float relativeTime = time / Mathf.Sqrt(1-(Mathf.Pow(MovingVelocity.magnitude, 2)/Mathf.Pow(Constants.v_light, 2)));
        return relativeTime;
    }
}
