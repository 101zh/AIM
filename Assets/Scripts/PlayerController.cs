using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float speed = 0.01f;
    CharacterController characterController;
    Mouse curMouse;
    Camera mainCam;
    [SerializeField] float x;
    [SerializeField] float y;
    float cameraVerticalRotation;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        curMouse = Mouse.current;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        x = curMouse.delta.x.value;
        y = curMouse.delta.y.value;

        cameraVerticalRotation -= y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        mainCam.transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        transform.Rotate(Vector3.up * x);
        MovePlayer();
    }

    void MovePlayer()
    {
        moveDir.z = Keyboard.current.wKey.isPressed ? 1 : 0;
        moveDir.z = (Keyboard.current.sKey.isPressed && moveDir.z == 0) ? -1 : moveDir.z;
        moveDir.x = Keyboard.current.aKey.isPressed ? -1 : 0;
        moveDir.x = (Keyboard.current.dKey.isPressed && moveDir.x == 0) ? 1 : moveDir.x;


        //moveDir += Physics.gravity;
        moveDir.Normalize();
        characterController.Move(moveDir * speed);
    }
}
