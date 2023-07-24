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
        if (!Input.GetButtonDown("Cancel") || section.GameFinished)
            return;

        if (section.GamePaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void ResumeGame()
    {
        pauseCanvas.enabled = false;
        section.ContinueGame();
        gameResumed.Invoke();
    }

    void PauseGame()
    {
        buttonController.SelectPauseButton();
        pauseCanvas.enabled = true;
        section.PauseGame();
        gamePaused.Invoke();
    }
}
