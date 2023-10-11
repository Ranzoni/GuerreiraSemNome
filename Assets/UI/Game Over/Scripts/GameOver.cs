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
    [SerializeField] UnityEvent gameResumed;
    [SerializeField] GameObject prefabButtonUINavSFX;
    [SerializeField] GameObject prefabButtonUIClickSFX;

    FirstButtonController buttonController;
    CheckpointManager checkpointManager;
    AudioSource buttonUINavSFX;
    AudioSource buttonUIClickSFX;

    void Start()
    {
        SetUIActive(false);

        buttonController = FindFirstObjectByType<FirstButtonController>();
        checkpointManager = FindFirstObjectByType<CheckpointManager>();
        buttonUINavSFX = AudioSourceInstantiate(prefabButtonUINavSFX);
        buttonUIClickSFX = AudioSourceInstantiate(prefabButtonUIClickSFX);
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
            StartCoroutine(ResumeGameRoutine());
        else
            section.StartGame();
    }

    IEnumerator ResumeGameRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);

        checkpointManager.RestoreToCheckpoint();
        SetUIActive(false);
        gameResumed.Invoke();
        section.UnlockScreen();
    }

    void SetUIActive(bool active)
    {
        for (var i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(active);
    }

    AudioSource AudioSourceInstantiate(GameObject prefab)
    {
        return Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
    }

    public void PlayButtonUINavSFX()
    {
        buttonUINavSFX.Play();
    }

    public void PlayButtonUIClickSFX()
    {
        buttonUIClickSFX.Play();
    }
}
