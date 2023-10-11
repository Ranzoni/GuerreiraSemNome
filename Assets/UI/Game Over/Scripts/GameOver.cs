using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [SerializeField] AudioSource principalMusic;

    FirstButtonController buttonController;
    CheckpointManager checkpointManager;
    AudioSource buttonUINavSFX;
    AudioSource buttonUIClickSFX;
    AudioSource gameOverSFX;

    void Start()
    {
        SetUIActive(false);

        buttonController = FindFirstObjectByType<FirstButtonController>();
        checkpointManager = FindFirstObjectByType<CheckpointManager>();
        buttonUINavSFX = AudioSourceInstantiate(prefabButtonUINavSFX);
        buttonUIClickSFX = AudioSourceInstantiate(prefabButtonUIClickSFX);
        gameOverSFX = GetComponent<AudioSource>();
    }    

    public void ExecuteGameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        gameFinished.Invoke();

        yield return new WaitForSeconds(gameOverDelay);

        principalMusic.Stop();
        gameOverSFX.Play();
        buttonController.SelectGameOverButton();
        SetUIActive(true);
        SetMenuButtonsActive(true);
        section.LockScreen();
    }

    public void ResumeGame()
    {
        SetMenuButtonsActive(false);

        if (checkpointManager is not null && checkpointManager.HasCheckpoint)
            StartCoroutine(ResumeGameRoutine());
        else
            section.StartGame();
    }

    void SetMenuButtonsActive(bool active)
    {
        var buttons = GetComponentsInChildren<Button>();
        Debug.Log(buttons.Length);
        foreach (var button in buttons)
            button.enabled = active;
    }

    IEnumerator ResumeGameRoutine()
    {
        yield return new WaitForSecondsRealtime(1f);

        checkpointManager.RestoreToCheckpoint();
        SetUIActive(false);
        gameResumed.Invoke();
        section.UnlockScreen();
        gameOverSFX.Stop();
        principalMusic.Play();
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
