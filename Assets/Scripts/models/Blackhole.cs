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
    public float gravity;
    
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
        transform.localScale = new Vector3(
            Constants.RS * 2 * Constants.SCALE,
            Constants.RS * 2 * Constants.SCALE,
            Constants.RS * 2 * Constants.SCALE
        );
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

            float forceX = gravity * (x - other.x);
            float forceY = gravity * (y - other.y);
            float forceZ = gravity * (z - other.z);
                
            other.SetAcceleration(forceX, forceY, forceZ);
        }
    }
}
