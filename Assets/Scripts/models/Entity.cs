using UnityEngine;

namespace DefaultNamespace.models
{
    /**
     * This class represents an entity (an object, e.g. a planet, a ship, etc.) in the game.
     */
    public class Entity: MonoBehaviour
    {
        // Position Coordinates of the object
        public float x, y, z;
        
        public float distanceToBlackhole;
        
        /**
         * This calculates the distance between this entity and another entity.
         */
        public float CalculateDistance(Entity other)
        {
            float thisX = transform.position.x;
            float thisY = transform.position.y;
            float thisZ = transform.position.z;
            
            float otherX = other.transform.position.x;
            float otherY = other.transform.position.y;
            float otherZ = other.transform.position.z;
            
            distanceToBlackhole = Mathf.Sqrt(
                Mathf.Pow(thisX - otherX, 2) +
                Mathf.Pow(thisY - otherY, 2) +
                Mathf.Pow(thisZ - otherZ, 2)
            );

            return distanceToBlackhole;
        }
    }
}
