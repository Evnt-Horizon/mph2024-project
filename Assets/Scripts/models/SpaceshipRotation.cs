using UnityEngine;

public class SpaceshipRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform point;

    // The planet's center

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction from the planet to the spaceship
        Vector3 directionToPlanet = transform.position - point.position;

        // Normalize the direction to get the normal (unit vector)
        Vector3 normalDirection = directionToPlanet.normalized;

        // Make the spaceship face the opposite direction of the planet (the normal vector)
        transform.rotation = Quaternion.LookRotation(-normalDirection);
    }
}
