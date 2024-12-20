using UnityEngine;
using Unity.Cinemachine;

public class MainCameraController : MonoBehaviour
{
    public CinemachineFollowZoom cinemachineFollowZoom;

    public CinemachineCamera mainCamera;
    public CinemachineCamera shipCamera;
    
    public SpaceshipEntity theShip;

    public Transform blackHole;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCamera.Priority = 11;
    }

    // Update is called once per frame
    void Update()
    {
        float zoomFactor = -Input.GetAxis("Mouse ScrollWheel");
        if(zoomFactor != 0)
        {
            cinemachineFollowZoom.Width = Mathf.Clamp(cinemachineFollowZoom.Width + zoomFactor, 0, 25);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (mainCamera.Priority == 11)
            {
                shipCamera.Priority = 11;
                mainCamera.Priority = 10;
                theShip.setScale(0.05f);
                blackHole.localScale = new Vector3(20, 20, 20);
            } else {
                mainCamera.Priority = 11;
                shipCamera.Priority = 10;
                theShip.setScale(0.5f);
                blackHole.localScale = new Vector3(10, 10, 10);
            }
        }
    }
    
    //To come back if we have extra time
    public float GetAxisCustom(string axisName) {
        if (axisName == "Mouse X")
        {
            Debug.Log("Getting X axis");
            if (Input.GetMouseButton(1))
            {
                Debug.Log("Mouse button clicked");
                return Input.GetAxis("Mouse X");
            }
            else
            {
                return 0;
            }
        } 
        if (axisName == "Mouse Y")
        {
            Debug.Log("Getting Y axis");
            if (Input.GetMouseButton(1))
            {
                Debug.Log("Mouse button clicked");
                return Input.GetAxis("Mouse Y");
            }
            else
            {
                return 0;
            }
        }

        return Input.GetAxis(axisName);
    }
}
