using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class Pause : MonoBehaviour
{
    [Tooltip("Prefab com o script para controle de sessão do jogo")]
    [SerializeField] ControlSection section;

    public UnityEvent gamePaused;
    public UnityEvent gameResumed;

    Canvas pauseCanvas;

    void Start()
    {
        pauseCanvas = GetComponent<Canvas>();
        pauseCanvas.enabled = false;
    }

    void Update()
    {
        if (!Input.GetButtonDown("Cancel"))
            return;

        if (section.GameIsStopped)
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
        pauseCanvas.enabled = true;
        section.StopGame();
        gamePaused.Invoke();
    }
}
