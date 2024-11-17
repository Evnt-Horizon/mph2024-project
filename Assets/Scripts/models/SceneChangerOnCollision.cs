using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneChangerOnCollision : MonoBehaviour
{
    // Name of the scene to load
    [SerializeField] private string sceneToLoad = "FinalScene";

    // Called when the object this script is attached to collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        // Replace "TargetObjectName" with the name of the object you want to detect collisions with
        if (collision.gameObject.name == "StarSparrow15")
        {
            Debug.Log("Collision detected with " + collision.gameObject.name);
            LoadScene();
        }
    }

    private void LoadScene()
    {
        // Check if the scene name is valid
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is not set or invalid.");
        }
    }
}
