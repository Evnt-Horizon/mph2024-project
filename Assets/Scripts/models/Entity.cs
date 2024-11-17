using UnityEngine;

namespace DefaultNamespace.models
{
    /**
     * This class represents an entity (an object, e.g. a planet, a ship, etc.) in the game.
     */
    public class Entity: MonoBehaviour
    {
        // Position Coordinates of the object
        public double x, y, z;
        
        /**
         * This calculates the distance between this entity and another entity.
         */
        public double CalculateDistance(Entity other)
        {
            double thisX = transform.position.x;
            double thisY = transform.position.y;
            double thisZ = transform.position.z;
            
            double otherX = other.transform.position.x;
            double otherY = other.transform.position.y;
            double otherZ = other.transform.position.z;
            
            double differenceX = thisX - otherX - Constants.RS;
            double differenceY = thisY - otherY - Constants.RS;
            double differenceZ = thisZ - otherZ - Constants.RS;

            return Mathf.Sqrt(
                Mathf.Pow((float) differenceX, 2) +
                Mathf.Pow((float) differenceY, 2) +
                Mathf.Pow((float) differenceZ, 2)
            );
        }
    }
}
