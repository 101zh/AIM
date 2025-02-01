using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCameraMovement : MonoBehaviour
{
    // Camera & Looking Variables
    [SerializeField] float mouseSensitvity = 0.25f;
    [SerializeField] Transform player;
    float cameraVerticalRotation;

    // Input Stuff
    InputAction look;

    // Start is called before the first frame update
    void Start()
    {
        look = GameManager.look;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lookDelta = look.ReadValue<Vector2>() * mouseSensitvity;

        cameraVerticalRotation -= lookDelta.y;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * lookDelta.x);
    }

    public void OnEnable()
    {
        if (look != null) look.Enable();
    }

    private void OnDisable()
    {
        look.Disable();
    }
}
