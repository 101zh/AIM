using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float mouseSensitvity = 0.5f;
    [SerializeField] float speed = 0.01f;
    CharacterController characterController;
    Mouse curMouse;
    Camera mainCam;
    float cameraVerticalRotation;

    AIMInput aimInput;
    InputAction move;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        aimInput = new AIMInput();
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        curMouse = Mouse.current;
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
        Debug.Log(transform.forward + ", " + transform.up + ", " + transform.right);

        moveDir = Vector3.zero;

        Vector2 playerMovement = move.ReadValue<Vector2>();

        moveDir += playerMovement.y * transform.forward;
        moveDir += playerMovement.x * transform.right;

        //moveDir += Physics.gravity;
        moveDir.Normalize();
        characterController.Move(moveDir * speed);
    }

    void RotatePlayerPersepective()
    {
        float x = curMouse.delta.x.value;
        float y = curMouse.delta.y.value;

        cameraVerticalRotation -= y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        mainCam.transform.localEulerAngles = Vector3.right * cameraVerticalRotation * mouseSensitvity;

        transform.Rotate(Vector3.up * x * mouseSensitvity);
    }

    public void OnEnable()
    {
        move = aimInput.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
}
