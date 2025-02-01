using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
    InputAction move;
    InputAction jump;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        move = GameManager.move;
        jump = GameManager.jump;
    }

    // Update is called once per frame
    void Update()
    {
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
            curVelocity.y = -2.5f; // Vertical velocity is still -2.5 to guarantee that player moves to ground
        }
        curVelocity.y += gravity * Time.deltaTime;

        characterController.Move(curVelocity * Time.deltaTime);
    }

    public void OnEnable()
    {
        if (move != null) move.Enable();
        if (jump != null) jump.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
}
