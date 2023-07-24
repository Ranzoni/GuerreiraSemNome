using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GameOver : MonoBehaviour
{
    [Tooltip("Tempo para apresentar o Game Over")]
    [SerializeField] float gameOverDelay = 1f;
    [Tooltip("Prefab com o script para controle de sessão do jogo")]
    [SerializeField] ControlSection section;

    Canvas gameOver;
    FirstButtonController buttonController;

    void Start()
    {
        gameOver = GetComponent<Canvas>();
        gameOver.enabled = false;

        buttonController = FindFirstObjectByType<FirstButtonController>();
    }    

    public void ExecuteGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(gameOverDelay);

        section.EndGame();
        buttonController.SelectGameOverButton();
        gameOver.enabled = true;
    }
}
