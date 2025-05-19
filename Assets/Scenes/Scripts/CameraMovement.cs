using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class CameraMovement : MonoBehaviour
{

    // Camera and movement settings
    public float rotationSensibility;
    public float movementSpeed;
    public float transitionSpeed = 5f;
    public float zoomSpeed;

    Vector2 rotation; // Stores camera rotation values

    [SerializeField] Camera cameraComponent; 

    void Start()
    {
        // Lock and hide the cursor for better camera control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        cameraComponent.fieldOfView = 60f; // Default FOV when not focusing on a planet

        HandleRotation();
        HandleMovement();
    }

    // Handles camera rotation based on mouse movement
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * rotationSensibility;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSensibility;

        // Adjust rotation direction based on camera orientation
        if (transform.up.y >= 0f)
            rotation.x += mouseX;
        else
            rotation.x -= mouseX;

        rotation.y -= mouseY;

        transform.rotation = Quaternion.Euler(rotation.y, rotation.x, 0);
    }

    // Handles movement based on keyboard input
    void HandleMovement()
    {
        Vector3 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            input.z += 1;
        if (Input.GetKey(KeyCode.S))
            input.z -= 1;
        if (Input.GetKey(KeyCode.A))
            input.x -= 1;
        if (Input.GetKey(KeyCode.D))
            input.x += 1;
        if (Input.GetKey(KeyCode.Space))
            input.y += 1;
        if (Input.GetKey(KeyCode.LeftShift))
            input.y -= 1;

        Vector3 movementDirection = GetMovementDirection(input);

        Vector3 movement = movementDirection * movementSpeed * Time.deltaTime;
        transform.position += movement;

        HandleZoom();
    }

    // Handles zooming in and out using the mouse scroll wheel
    void HandleZoom()
    {
        float zoom = 0.0f;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            zoom += zoomSpeed;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            zoom -= zoomSpeed;

        transform.position += transform.forward * zoom;
    }

    // Converts input into a movement direction
    Vector3 GetMovementDirection(Vector3 input)
    {
        Vector3 movementDirection = transform.forward * input.z + transform.right * input.x + transform.up * input.y;
        return movementDirection.normalized;
    }
}
