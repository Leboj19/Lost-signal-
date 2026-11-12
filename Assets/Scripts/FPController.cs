using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class FPController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f; // Controls the downward force
    //applied to the player.The value is negative because gravity pulls the player down.

    public float jumpHeight = 1.5f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform gunPoint;
    public float bulletForce = 500f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    public float crouchSpeed = 2.5f;
    private float originalMoveSpeed;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;

    public bool canMove = true; // Determines if the player can move or not.
    private Vector3 velocity; // Stores the player's current vertical movement, including gravity.
    private float verticalRotation = 0f;
    // Awake runs once when the GameObject is first loaded.

    private bool gameFinished = false;
    private bool gameplayInputBlocked = false;


    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public Transform holdPoint;
    private PickupObject heldObject;

    [Header("Interaction")]
    public float interactionDistance = 3f;
    public Camera playerCamera;
    public InputActionReference interactAction;

    public GameObject interactionPrompt;

    [Header("Sprint Settings")]
    public float sprintSpeed = 8f;
    private bool isSprinting = false;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        originalMoveSpeed = moveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (gameFinished)
            return;

        if (gameplayInputBlocked)
        {
            moveInput = Vector2.zero;
            lookInput = Vector2.zero;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            return;
        }
        
        if (!canMove)
        {//Stop movement and looking if the player is not allowed to move.
            moveInput = Vector2.zero;
            lookInput = Vector2.zero;

            if(interactionPrompt != null)interactionPrompt.SetActive(false);

            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Keeps the player grounded when not moving.
            }
            velocity.y += gravity * Time.deltaTime; // Applies gravity to the player.
            controller.Move(velocity * Time.deltaTime); // Moves the player based on gravity.

            return;

        }
        HandleMovement();
        HandleLook();
        CheckForInteraction();

        if (heldObject != null)
        {
            heldObject.MoveToHoldPoint(holdPoint.position);
        }

        if (interactAction != null && interactAction.action.WasPressedThisFrame())
        {
            OnInteract();
        }
    





    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // Reads the movement input as a Vector2.
        // For example, WASD or the left analogue stick.
        moveInput = context.ReadValue<Vector2>();
    }
    // This method is called by the Input System when look input changes.

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
        else if (context.canceled)
        {
            isSprinting = false;
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        // Reads the look input as a Vector2.
        // For example, mouse movement or the right analogue stick.
        lookInput = context.ReadValue<Vector2>();
    }
    // Handles the player's movement and gravity.
    public void HandleMovement()
    {
        // Creates the horizontal movement direction.
        Vector3 move =
        transform.right * moveInput.x +
        transform.forward * moveInput.y;
        // Moves the player horizontally.
        
        float currentSpeed = moveSpeed;
        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);
        
        // Keeps the player connected to the ground.
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Gravity must run every frame, not only while grounded.
        velocity.y += gravity * Time.deltaTime;
        // Applies the vertical movement for jumping and falling.
        controller.Move(velocity * Time.deltaTime);
    }
    // Handles the player's camera and body rotation.
    public void HandleLook()
    {
        // Calculates horizontal camera movement using the look input
        // and the selected sensitivity.
        float mouseX = lookInput.x * lookSensitivity;
        // Calculates vertical camera movement using the look input
        // and the selected sensitivity.
        float mouseY = lookInput.y * lookSensitivity;
        // Subtracts the vertical mouse movement from the camera rotation.
        // Subtraction makes moving the mouse upwards look upwards.
        verticalRotation -= mouseY;
        // Limits the vertical camera rotation so that the player
        // cannot rotate the camera completely over their head.
        verticalRotation = Mathf.Clamp(
        verticalRotation,
        -verticalLookLimit,
        verticalLookLimit
        );
        // Rotates only the camera up and down.
        cameraTransform.localRotation =
        Quaternion.Euler(verticalRotation, 0f, 0f);
        // Rotates the entire player GameObject left and right.
        transform.Rotate(Vector3.up * mouseX);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded) // Check that theJump action was successfully performed and that the player is currently standing on the ground.
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculates the upward speed needed for the player to reach the chosen jump height while accounting for gravity.
        }
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (bulletPrefab != null && gunPoint != null)
        {
            GameObject bullet = Instantiate(
            bulletPrefab,
            gunPoint.position,
            gunPoint.rotation
            );
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(gunPoint.forward * bulletForce); // Adjust force value as needed
            }
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = crouchHeight;
            moveSpeed = crouchSpeed;
        }
        else if (context.canceled)
        {
            controller.height = standHeight;
            moveSpeed = originalMoveSpeed;
        }


    }
    public void OnPickup(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (heldObject == null)
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
            {
                PickupObject pickup = hit.collider.GetComponent<PickupObject>();
                if (pickup != null)
                {
                    pickup.Pickup(holdPoint);
                    heldObject = pickup;
                }
            }
        }
        else
        {
            heldObject.Drop();
            heldObject = null;
        }
    }

    public void OnInteract()
    {
        if(!canMove) return;
        Ray ray = new Ray(
       cameraTransform.position,
       cameraTransform.forward
   );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Debug.Log("Interacted with: " + hit.collider.gameObject.name);

            //  Double Checks if the object has an IInteractable component
            IInteractable interactable =
                hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
    void CheckForInteraction()
    {
        if (interactionPrompt == null || cameraTransform == null)
            return;

        Ray ray = new Ray(
            cameraTransform.position,
            cameraTransform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable =
                hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactionPrompt.SetActive(true);

                InteractionPromptText prompt =
               hit.collider.GetComponent<InteractionPromptText>();

                if (prompt != null)
                {
                    interactionPrompt.GetComponent<TMPro.TMP_Text>().text =
                        prompt.promptText;
                }
                else
                {
                    interactionPrompt.GetComponent<TMPro.TMP_Text>().text =
                        "PRESS E TO INTERACT";
                }

                return;
            }
        }

        interactionPrompt.SetActive(false);

    }
    public void SetGameplayInputBlocked(bool blocked)
    {
        gameplayInputBlocked = blocked;

        moveInput = Vector2.zero;
        lookInput = Vector2.zero;

        if (blocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void StopPlayer()
    {
        gameFinished = true;

        moveInput = Vector2.zero;
        lookInput = Vector2.zero;

        velocity = Vector3.zero;

        if(interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
        Debug.Log("PLAYER FROZEN.");
    }
}

   
