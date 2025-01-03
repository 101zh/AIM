using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform mainCam;
    [SerializeField] LayerMask targetLayerMask;

    // Input Stuff
    AIMInput aimInput;
    InputAction fire;

    private void Awake()
    {
        aimInput = new AIMInput();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fire.WasPressedThisFrame()) FireRaycastShot();
    }

    void FireRaycastShot()
    {
        Ray ray = new Ray(mainCam.position, mainCam.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, targetLayerMask.value))
        {
            Debug.Log("ray hit target");
            Destroy(hitInfo.transform.gameObject);
        }
    }
    public void OnEnable()
    {
        fire = aimInput.Player.Fire;
        fire.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
    }
}
