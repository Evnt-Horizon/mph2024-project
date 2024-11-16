using UnityEngine;
using Unity.Cinemachine;

public class MainCameraController : MonoBehaviour
{
    public CinemachineFollowZoom cinemachineFollowZoom;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float zoomFactor = Input.GetAxis("Mouse ScrollWheel");
        if(zoomFactor != 0)
        {
            cinemachineFollowZoom.Width = Mathf.Clamp(cinemachineFollowZoom.Width + zoomFactor, 2, 10);
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
