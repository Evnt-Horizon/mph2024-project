using System.Collections.Generic;
using DefaultNamespace.models;

/**
 * This is a class that contains all the logic for something that has a gravitational pull.
 * It will contain all properties (things in its gravity, its gravity force, etc.)
 * and methods (updating other objects in its gravity, etc.) for objects with gravity.
 */
public class ObjectWithGravity : Entity
{
    // Gravity force of the object, as a vector that is always pointing towards the object (Fg).
    public double gravityForce;
    // Gravity range of the object
    public double gravityRange;
    
    // List of objects that is in relation to the object's gravity
    public List<MovingObject> objectsInGravity;
    
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
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        
        for (int i = 0; i < objectsInGravity.Count; i++)
        {
            MovingObject other = objectsInGravity[i];
            
            double distance = CalculateDistance(other);
            if (distance <= gravityRange)
            {
                double force = gravityForce * (distance / gravityRange);
                double forceX = force * (x - other.x) / distance;
                double forceY = force * (y - other.y) / distance;
                double forceZ = force * (z - other.z) / distance;
                
                other.SetAcceleration(forceX, forceY, forceZ);
            }
        }
    }
}
