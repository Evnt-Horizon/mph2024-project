using System;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{
    public CinemachineFollowZoom cinemachineFollowZoom;
    
    [Header("Variables")]
    public float verticalSpeed = 2.0f;
    public float horizontalSpeed = 2.0f;

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
