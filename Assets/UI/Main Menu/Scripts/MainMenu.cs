using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject prefabButtonUINavSFX;
    [SerializeField] GameObject prefabButtonUIClickSFX;
    [SerializeField] ControlSection section;

    AudioSource buttonUINavSFX;
    AudioSource buttonUIClickSFX;

    void Start()
    {
        buttonUINavSFX = AudioSourceInstantiate(prefabButtonUINavSFX);
        buttonUIClickSFX = AudioSourceInstantiate(prefabButtonUIClickSFX);
    }

    public void StartGame()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons)
            button.enabled = false;
            
        section.StartGame();
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
