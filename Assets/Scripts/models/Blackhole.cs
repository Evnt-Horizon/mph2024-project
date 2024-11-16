using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.models;
using UnityEngine;

/**
 * This is a class that contains all the logic for something that has a gravitational pull.
 * It will contain all properties (things in its gravity, its gravity force, etc.)
 * and methods (updating other objects in its gravity, etc.) for objects with gravity.
 */
public class Blackhole : Entity
{
    // Gravity force of the object, as a vector that is always pointing towards the object (Fg).
    public float gravityForce;
    
    // List of objects that is in relation to the object's gravity
    public List<Spaceship> objectsInGravity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the position of the object by reading the transform component of the object
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        
        // Scale it to proper radius
        transform.localScale = new Vector3((float)Constants.RS * 2, (float)Constants.RS * 2, (float)Constants.RS * 2);
    }

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        
        for (int i = 0; i < objectsInGravity.Count; i++)
        {
            Spaceship other = objectsInGravity[i];
            
            // We will always make y = 0 as we only work with x and z as a 2D plane
            
            float deltaX = other.x - x;
            float deltaZ = other.z - z;
            
            // Calculate the angle between the centripital directional vector and the x-axis
            float angleX = Mathf.Atan(deltaX / deltaZ);
            // Calculate the angle between the centripital directional vector and the z-axis
            float angleZ = Mathf.Atan(deltaZ / deltaX);


            float forceX = Mathf.Abs(Mathf.Cos(angleX) * gravityForce);
            float forceY = 0;
            float forceZ = Mathf.Abs(Mathf.Sin(angleZ) * gravityForce);
            
            // Adjust angles based on the quadrant
            if (other.x >= 0)
            {
                forceX = -forceX;
            }
            if (other.z >= 0)
            {
                forceZ = -forceZ;
            }
                
            other.SetAcceleration(forceX, forceY, forceZ);
        }
    }
}
