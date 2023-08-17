using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class GameOver : MonoBehaviour
{
    [Tooltip("Tempo para apresentar o Game Over")]
    [SerializeField] float gameOverDelay = 1f;
    [Tooltip("Prefab com o script para controle de sessão do jogo")]
    [SerializeField] ControlSection section;
    [SerializeField] UnityEvent gameFinished;

    Canvas gameOverCanvas;
    FirstButtonController buttonController;

    void Start()
    {
        gameOverCanvas = GetComponent<Canvas>();
        gameOverCanvas.enabled = false;

        buttonController = FindFirstObjectByType<FirstButtonController>();
    }    

    public void ExecuteGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        gameFinished.Invoke();

        yield return new WaitForSeconds(gameOverDelay);

        section.LockScreen();
        buttonController.SelectGameOverButton();
        gameOverCanvas.enabled = true;
    }

    public void ResumeGame()
    {
        section.StartGame();
    }
}
