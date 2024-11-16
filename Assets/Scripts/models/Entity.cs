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
            
            float differenceX = thisX - otherX - Constants.RS;
            float differenceY = thisY - otherY - Constants.RS;
            float differenceZ = thisZ - otherZ - Constants.RS;

            return Mathf.Sqrt(
                Mathf.Pow(differenceX, 2) +
                Mathf.Pow(differenceY, 2) +
                Mathf.Pow(differenceZ, 2)
            );
        }
    }
}
