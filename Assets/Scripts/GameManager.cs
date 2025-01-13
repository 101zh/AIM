using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    // Input Stuff
    public static AIMInput aimInput;
    public static InputAction look;
    public static InputAction fire;
    public static InputAction jump;
    public static InputAction move;

    private void Awake()
    {
        aimInput = new AIMInput();
        look = aimInput.Player.Look;
        fire = aimInput.Player.Fire;
        jump = aimInput.Player.Jump;
        move = aimInput.Player.Move;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
