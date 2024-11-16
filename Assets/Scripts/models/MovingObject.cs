using DefaultNamespace.models;
using UnityEngine;

/**
 * This is a class that contains all the logic for moving objects in the scene.
 * It will contain all properties (position velocity, acceleration, etc.) and methods (updating position, etc.)
 * for moving objects.
 */
public class MovingObject : Entity
{
    // Velocity of the object
    public double vx, vy, vz;
    // Acceleration of the object
    public double ax, ay, az;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the position of the object by reading the transform component of the object
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the velocity of the object by adding the acceleration to the velocity
        vx += ax;
        vy += ay;
        vz += az;
        
        // Update the position of the object by adding the velocity to the position
        x += vx;
        y += vy;
        z += vz;
        
        // Update the transform component of the object with the new position
        transform.position = new Vector3((float)x, (float)y, (float)z);
    }
    
    public void SetAcceleration(double ax, double ay, double az)
    {
        this.ax = ax;
        this.ay = ay;
        this.az = az;
    }
}
