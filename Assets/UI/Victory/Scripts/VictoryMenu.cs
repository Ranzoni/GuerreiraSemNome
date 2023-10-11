using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] GameObject prefabButtonUISFX;

    AudioSource buttonUISFX;

    void Start()
    {
        buttonUISFX = Instantiate(prefabButtonUISFX, transform.position, Quaternion.identity).GetComponent<AudioSource>();
    }

    public void PlayButtonUISFX()
    {
        buttonUISFX.Play();
    }
}
