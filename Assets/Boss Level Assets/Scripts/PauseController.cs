using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    private InputActions inputActions;
    private bool isPaused = false;
    public CinemachineVirtualCamera cinemachineCamera;
    private void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Pause.performed += TogglePause;
    }

    private void OnDisable()
    {
        inputActions.UI.Pause.performed -= TogglePause;
        inputActions.UI.Disable();
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        cinemachineCamera.enabled = !cinemachineCamera.enabled;
        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }
}