using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] GameObject prefabButtonUINavSFX;
    [SerializeField] GameObject prefabButtonUIClickSFX;

    AudioSource buttonUINavSFX;
    AudioSource buttonUIClickSFX;

    void Start()
    {
        buttonUINavSFX = AudioSourceInstantiate(prefabButtonUINavSFX);
        buttonUIClickSFX = AudioSourceInstantiate(prefabButtonUIClickSFX);
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
