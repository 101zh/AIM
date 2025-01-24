using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false;

    [ContextMenuItem("updateAverageStats", "updateAverageStats")]
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
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else if (!isPaused)
            {
                PauseGame();
            }

        }
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        pauseMenu.SetActive(true);
        aimInput.Player.Disable();
    }

    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        aimInput.Player.Enable();
    }

    [ContextMenu("updateAverageStats")]
    void updateAverageStats()
    {
        PracticeStats averagePracticeStats = PracticeSessionManager.instance.GetAveragePracticeStats();
        averageShotDelay = averagePracticeStats.shotDelay;
        averageMisses = averagePracticeStats.missFires;
        averageOvershoot = averagePracticeStats.overShoot;
        averageTimeToKillTarget = averagePracticeStats.timeToKillTarget;
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
