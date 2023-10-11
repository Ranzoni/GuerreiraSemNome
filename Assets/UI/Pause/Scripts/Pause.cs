using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class Pause : MonoBehaviour
{
    [Tooltip("Prefab com o script para controle de sessão do jogo")]
    [SerializeField] ControlSection section;
    [SerializeField] UnityEvent gamePaused;
    [SerializeField] UnityEvent gameResumed;
    [SerializeField] GameObject prefabButtonUINavSFX;
    [SerializeField] GameObject prefabButtonUIClickSFX;

    FirstButtonController buttonController;
    AudioSource buttonUINavSFX;
    AudioSource buttonUIClickSFX;
    AudioSource sfx;

    void Start()
    {
        SetUIActive(false);

        buttonController = FindFirstObjectByType<FirstButtonController>();
        buttonUINavSFX = AudioSourceInstantiate(prefabButtonUINavSFX);
        buttonUIClickSFX = AudioSourceInstantiate(prefabButtonUIClickSFX);
        sfx = GetComponent<AudioSource>();
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
        sfx.Play();
        SetUIActive(false);
        section.UnlockScreen();
        gameResumed.Invoke();
    }

    public void RestartGame()
    {
        DisableMenu();
        section.StartGame();
    }

    void DisableMenu()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
            button.enabled = false;
    }

    void PauseGame()
    {
        sfx.Play();
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
