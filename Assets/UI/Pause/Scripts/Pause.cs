using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class Pause : MonoBehaviour
{
    [Tooltip("Prefab com o script para controle de sessão do jogo")]
    [SerializeField] ControlSection section;
    [SerializeField] UnityEvent gamePaused;
    [SerializeField] UnityEvent gameResumed;

    FirstButtonController buttonController;

    void Start()
    {
        SetUIActive(false);

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
        return transform.GetChild(0).gameObject.activeSelf;
    }

    public void ResumeGame()
    {
        SetUIActive(false);
        section.UnlockScreen();
        gameResumed.Invoke();
    }

    void PauseGame()
    {
        buttonController.SelectPauseButton();
        SetUIActive(true);
        section.LockScreen();
        gamePaused.Invoke();
    }

    void SetUIActive(bool active)
    {
        for (var i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(active);
    }
}
