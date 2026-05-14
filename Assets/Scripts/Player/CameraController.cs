using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private CinemachineThirdPersonFollow playerCam;
    [SerializeField] private float sensitivity = 15f;
    [SerializeField] private float orientationSmooth = 5f;
    [SerializeField] private float limitX = 90f;

    [SerializeField] private Vector2 cameraDistanceLimit = new Vector2(2 , 6);
    private Vector2 input;
    private float xRotation;
    private float yRotation;

    

    private void Awake()
    {
        InputManager.Controls.Player.Look.performed += ctx => input = ctx.ReadValue<Vector2>(); 
        InputManager.Controls.Player.Look.canceled += ctx => input = Vector2.zero; 

        //InputManager.Controls.Player.Zoom.performed += Zoom; 
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void Update()
    {
        Look(input);    
    }

    private void Look(Vector2 input)
    {
        float mouseX = input.x * sensitivity * Time.deltaTime;
        float mouseY = input.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation , -limitX , limitX);

        orientation.rotation = Quaternion.Lerp(orientation.rotation , Quaternion.Euler(0 , yRotation , 0) , orientationSmooth * Time.deltaTime);


        cameraHolder.rotation = Quaternion.Euler(xRotation , yRotation , 0);

    }

    private void Zoom(InputAction.CallbackContext ctx)
    {
        float distance = playerCam.CameraDistance;
        distance -= ctx.ReadValue<Vector2>().y;
        distance = Math.Clamp(distance , cameraDistanceLimit.x , cameraDistanceLimit.y);
        playerCam.CameraDistance = distance;
    }

    public void MaxZoom()
    {
        playerCam.CameraDistance = cameraDistanceLimit.y;
    }
}
