using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Camera & Looking Variables
    [SerializeField] float mouseSensitvity = 0.25f;
    Camera mainCam;
    float cameraVerticalRotation;

    //Movement Stuff
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpHeight = 1.75f;
    [SerializeField] float gravity = -9.81f;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.1f;
    [SerializeField] LayerMask groundMask;

    CharacterController characterController;
    bool isGrounded;
    Vector3 curVelocity;

    // Input Stuff
    AIMInput aimInput;
    InputAction look;
    InputAction move;
    InputAction jump;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        aimInput = new AIMInput();
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayerPersepective();
        MovePlayer();
    }

    void MovePlayer()
    {
        // X & Z Movement
        Vector2 movementInput = move.ReadValue<Vector2>();
        moveDir = movementInput.y * transform.forward + movementInput.x * transform.right;
        moveDir.Normalize();

        characterController.Move(moveDir * speed * Time.deltaTime);

        // Y Movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Jumping
        if (jump.IsPressed() && isGrounded)
        {
            curVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // If player reaches ground and is still falling
        if (isGrounded && curVelocity.y < 0)
        {
            curVelocity.y = -1f; // Vertical velocity is still -1 to guarantee that player moves to ground
        }
        curVelocity.y += gravity * Time.deltaTime;

        characterController.Move(curVelocity * Time.deltaTime);
    }

    void RotatePlayerPersepective()
    {
        Vector2 lookDelta = look.ReadValue<Vector2>();

        cameraVerticalRotation -= lookDelta.y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        mainCam.transform.localEulerAngles = Vector3.right * cameraVerticalRotation * mouseSensitvity;

        transform.Rotate(Vector3.up * lookDelta.x * mouseSensitvity);
    }

    public void OnEnable()
    {
        move = aimInput.Player.Move;
        jump = aimInput.Player.Jump;
        look = aimInput.Player.Look;
        move.Enable();
        jump.Enable();
        look.Enable();
    }

    private void OnDisable()
    {
        look.Disable();
        move.Disable();
        jump.Enable();
    }
}
