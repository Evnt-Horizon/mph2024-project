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

            return Mathf.Sqrt(
                Mathf.Pow((float)(thisX - otherX), 2) +
                Mathf.Pow((float)(thisY - otherY), 2) +
                Mathf.Pow((float)(thisZ - otherZ), 2)
            );
        }
    }
}
