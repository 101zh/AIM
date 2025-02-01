using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] ResultsMenu resultsMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject controlsMenu;
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

        aimInput = new AIMInput();
        look = aimInput.Player.Look;
        fire = aimInput.Player.Fire;
        jump = aimInput.Player.Jump;
        move = aimInput.Player.Move;
    }

    private void Start()
    {
        aimInput.Player.Disable();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
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

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        aimInput.Player.Enable();
    }

    public void endPracticeSession()
    {
        resultsMenu.activateAndUpdateVlaues(PracticeSessionManager.instance.GetAveragePracticeStats());
        aimInput.Player.Disable();
        Cursor.lockState = CursorLockMode.None;
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

    public void StartSession()
    {
        Time.timeScale = 1;
        aimInput.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        mainMenu.SetActive(false);
    }

    public void quitApp()
    {
        Application.Quit();
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
