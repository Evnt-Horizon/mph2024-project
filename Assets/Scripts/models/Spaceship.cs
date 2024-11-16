using System;
using DefaultNamespace;
using DefaultNamespace.models;
using Unity.Mathematics.Geometry;
using UnityEngine;

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
        
        
        // Update the velocity of the object by adding the acceleration to the velocity
        vx += ax;
        vy += ay;
        vz += az;
        
        // Update the position of the object by adding the velocity to the position
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

    public Vector3 LorentzVelocityTransformation(Vector3 movingVelocity, Vector3 frameVelocity)
    {
        float v = Vector3.Magnitude(frameVelocity);
        
        if (v > 0.0)
        {
            Vector3 velocityAxis = -frameVelocity / v;
            float gamma = 1.0f / Mathf.Sqrt(1.0f - v * v);
            
            float movingParameter = Vector3.Dot(movingVelocity, velocityAxis);
            Vector3 movingPerpendicular = movingVelocity - velocityAxis * movingParameter;

            float denom = 1.0f + v * movingParameter;
            return (velocityAxis * (movingParameter + v) + movingPerpendicular / gamma) / denom;

        }
        
        
        return movingVelocity;
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
    
}