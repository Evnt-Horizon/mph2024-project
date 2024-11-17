using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneChangerOnCollision : MonoBehaviour
{
    // Name of the scene to load
    [SerializeField] private string sceneToLoad = "FinalScene";

    // Called when the object this script is attached to collides with another object
    private void OnTriggerEnter(Collider collider)
    {
               
     
            Debug.Log("Collision detected with " + collider.gameObject.name);
            LoadScene();
       
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
