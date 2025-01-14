using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform mainCam;
    [SerializeField] LayerMask targetLayerMask;

    // Input Stuff
    InputAction fire;

    // Start is called before the first frame update
    void Start()
    {
        fire = GameManager.fire;
        OnEnable();
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
        if (fire != null) fire.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
    }
}
