using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] Transform mainCam;
    [SerializeField] LayerMask targetLayerMask;

    PracticeSessionManager sessionManagerInstance;

    bool trackTimeToKillTarget = false;
    bool canHitTarget = false;
    bool couldHitTarget = false;

    float timeToKillTarget = 0.0f;
    int missFireCount = 0;
    Vector2 overShoot = Vector2.zero;
    float shotDelay = 0.0f;

    // Input Stuff
    InputAction fire;
    InputAction look;

    // Start is called before the first frame update
    void Start()
    {
        fire = GameManager.fire;
        look = GameManager.look;
        sessionManagerInstance = PracticeSessionManager.instance;
        sessionManagerInstance.onTargetInView += startTrackingTimeToKillTarget;
        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {
        FireRaycastShot();

        if (couldHitTarget)
        {
            shotDelay += Time.deltaTime;
            if (!canHitTarget)
            {
                Vector2 mouseInput = look.ReadValue<Vector2>();
                mouseInput.y = Mathf.Abs(mouseInput.y);
                mouseInput.x = Mathf.Abs(mouseInput.x);
                overShoot += mouseInput;
            }
        }
        if (trackTimeToKillTarget)
        {
            timeToKillTarget += Time.deltaTime;
        }
    }

    void FireRaycastShot()
    {
        Ray ray = new Ray(mainCam.position, mainCam.forward);
        RaycastHit hitInfo;
        canHitTarget = Physics.Raycast(ray, out hitInfo, Mathf.Infinity) && hitInfo.transform.gameObject.layer == Mathf.Log(targetLayerMask.value, 2);

        if (canHitTarget)
        {
            Debug.Log("can hit target");
            couldHitTarget = true;
        }

        if (fire.WasPressedThisFrame())
        {
            if (canHitTarget)
            {
                Debug.Log("shot target");

                Destroy(hitInfo.transform.gameObject);

                sendPerTargetVariablesToManager();
                resetPerTargetVariables();
                sessionManagerInstance.createNewTarget();
            }
            else
            {
                missFireCount += 1;
            }
        }
    }

    void sendPerTargetVariablesToManager()
    {
        sessionManagerInstance.addNewPraticeDataSet(new PracticeStats(shotDelay + Time.deltaTime, missFireCount, overShoot.magnitude, timeToKillTarget));
    }

    void resetPerTargetVariables()
    {
        trackTimeToKillTarget = false;
        timeToKillTarget = 0.0f;
        missFireCount = 0;
        overShoot = Vector2.zero;
        couldHitTarget = false;
        shotDelay = 0.0f;
    }

    public void startTrackingTimeToKillTarget()
    {
        trackTimeToKillTarget = true;
    }

    public void OnEnable()
    {
        if (fire != null) fire.Enable();
        if (look != null) look.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
    }
}
