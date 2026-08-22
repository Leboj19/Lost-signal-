
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
public class PlayerMovement : MonoBehaviour
{
    public float walking = 7f;
    public float running = 14f;
    public float jumping =8f ;
    public Transform playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float gravity = 10f;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController; 

    private bool CanMove = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right= transform.TransformDirection(Vector3.right);



        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = CanMove ? (isRunning ? running : walking ) * Input.GetAxis("Vertical"): 0;
        float curSpeedY = CanMove ? (isRunning ? running : walking ) * Input.GetAxis("Horizontal"): 0;
        float movementDirectionY = moveDirection.y; 
        moveDirection = (forward * curSpeedX ) + (right * curSpeedY);

        if (Input.GetButton("Jump") && CanMove && characterController.isGrounded)
        {
            moveDirection.y = jumping;
        }
        else
        {
            moveDirection.y= movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (CanMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X")* lookSpeed,0);
        }

    }
}
