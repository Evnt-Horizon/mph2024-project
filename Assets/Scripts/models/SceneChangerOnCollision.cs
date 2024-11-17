using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class SceneChangerOnCollision : MonoBehaviour
{
    // Name of the scene to load
    [SerializeField] private string sceneToLoad = "FinalScene";

    // Called when the object this script is attached to collides with another object
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Collision"); 
        SceneManager.LoadScene(sceneToLoad);
    }
}
