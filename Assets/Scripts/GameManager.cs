using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [ContextMenuItem("updateAverageShotDelay", "updateAverageShotDelay")]
    [SerializeField] float averageShotDelay;

    [ContextMenuItem("updateAverageMisses", "updateAverageMisses")]
    [SerializeField] float averageMisses;

    [ContextMenuItem("updateAverageOvershoot", "updateAverageOvershoot")]
    [SerializeField] float averageOvershoot;

    [ContextMenuItem("updateAverageTimeToKillTarget", "updateAverageTimeToKillTarget")]
    [SerializeField] float averageTimeToKillTarget;

    // Input Stuff
    public static AIMInput aimInput;
    public static InputAction look;
    public static InputAction fire;
    public static InputAction jump;
    public static InputAction move;

    private void Awake()
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

        aimInput = new AIMInput();
        look = aimInput.Player.Look;
        fire = aimInput.Player.Fire;
        jump = aimInput.Player.Jump;
        move = aimInput.Player.Move;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("updateAverageShotDelay")]
    void updateAverageShotDelay()
    {
        averageShotDelay = PracticeSessionManager.instance.getAverageShotDelay();
    }

    [ContextMenu("updateAverageMisses")]
    void updateAverageMisses()
    {
        averageMisses = PracticeSessionManager.instance.getAverageMisses();
    }

    [ContextMenu("updateAverageOvershoot")]
    void updateAverageOvershoot()
    {
        averageOvershoot = PracticeSessionManager.instance.getAverageOverShoot();
    }

    [ContextMenu("updateAverageTimeToKillTarget")]
    void updateAverageTimeToKillTarget()
    {
        averageTimeToKillTarget = PracticeSessionManager.instance.getAverageTimeToKillTarget();
    }
}
