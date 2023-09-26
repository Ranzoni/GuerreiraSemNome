using UnityEngine;

public class BanditSFXController : MonoBehaviour
{
    [SerializeField] GameObject prefabAttackSFX;
    [SerializeField] GameObject prefabHurtSFX;
    [SerializeField] GameObject prefabDeathSFX;

    AudioSource attackSFX;
    AudioSource hurtSFX;
    AudioSource deathSFX;

    void Start()
    {
        attackSFX = GetAudioSourceInstance(prefabAttackSFX);
        hurtSFX = GetAudioSourceInstance(prefabHurtSFX);
        deathSFX = GetAudioSourceInstance(prefabDeathSFX);
    }

    AudioSource GetAudioSourceInstance(GameObject prefab)
    {
        var instanciaSFX = Instantiate(prefab, transform);
        return instanciaSFX.GetComponent<AudioSource>();
    }

    public void PlayAttackSFX()
    {
        attackSFX.Play();
    }

    public void PlayHurtSFX()
    {
        hurtSFX.Play();
    }

    public void PlayDeathSFX()
    {
        deathSFX.Play();
    }
}
