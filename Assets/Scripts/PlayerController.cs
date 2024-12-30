using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 moveDir = Vector3.zero;
    [SerializeField] float speed = 0.01f;
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
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
