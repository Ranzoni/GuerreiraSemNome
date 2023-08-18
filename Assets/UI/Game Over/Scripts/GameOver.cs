using System.Collections;
using Cinemachine;
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
    [SerializeField] UnityEvent gameResumed;

    FirstButtonController buttonController;
    CheckpointManager checkpointManager;

    void Start()
    {
        SetUIActive(false);

        buttonController = FindFirstObjectByType<FirstButtonController>();
        checkpointManager = FindFirstObjectByType<CheckpointManager>();
    }    

    public void ExecuteGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        gameFinished.Invoke();

        yield return new WaitForSeconds(gameOverDelay);

        buttonController.SelectGameOverButton();
        SetUIActive(true);
        section.LockScreen();
    }

    public void ResumeGame()
    {
        if (checkpointManager is not null && checkpointManager.HasCheckpoint)
        {
            checkpointManager.RestoreToCheckpoint();
            SetUIActive(false);
            gameResumed.Invoke();
            section.UnlockScreen();
        }
        else
            section.StartGame();
    }

    void SetUIActive(bool active)
    {
        for (var i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(active);
    }
}
