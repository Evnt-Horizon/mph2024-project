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
}
