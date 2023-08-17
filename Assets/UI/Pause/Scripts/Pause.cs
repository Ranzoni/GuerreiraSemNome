using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class Pause : MonoBehaviour
{
    [Tooltip("Prefab com o script para controle de sessão do jogo")]
    [SerializeField] ControlSection section;
    [SerializeField] UnityEvent gamePaused;
    [SerializeField] UnityEvent gameResumed;

    Canvas pauseCanvas;
    FirstButtonController buttonController;

    void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
        pauseCanvas.enabled = false;

        buttonController = FindFirstObjectByType<FirstButtonController>();
    }

    void Update()
    {
        if (!Input.GetButtonDown("Cancel"))
            return;

        if (IsPaused())
            ResumeGame();
        else
            PauseGame();
    }

    bool IsPaused()
    {
        return pauseCanvas.enabled;
    }

    public void ResumeGame()
    {
        pauseCanvas.enabled = false;
        section.UnlockScreen();
        gameResumed.Invoke();
    }

    void PauseGame()
    {
        buttonController.SelectPauseButton();
        pauseCanvas.enabled = true;
        section.LockScreen();
        gamePaused.Invoke();
    }
}
