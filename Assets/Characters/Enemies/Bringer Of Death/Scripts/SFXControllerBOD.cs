using UnityEngine;

public class SFXControllerBOD : MonoBehaviour
{
    [SerializeField] GameObject prefabAttackSFX;
    [SerializeField] GameObject prefabSpellSFX;
    [SerializeField] GameObject prefabHurtSFX;
    [SerializeField] GameObject prefabDeathSFX;

    AudioSource attackSFX;
    AudioSource spellSFX;
    AudioSource hurtSFX;
    AudioSource deathSFX;

    void Start()
    {
        attackSFX = GetAudioSourceInstance(prefabAttackSFX);
        spellSFX = GetAudioSourceInstance(prefabSpellSFX);
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

    public void PlaySpellSFX()
    {
        spellSFX.Play();
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
