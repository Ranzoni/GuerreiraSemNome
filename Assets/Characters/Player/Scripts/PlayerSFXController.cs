using UnityEngine;

public class PlayerSFXController : MonoBehaviour
{
    [SerializeField] GameObject prefabJumpSFX;
    [SerializeField] GameObject prefabDashSFX;
    [SerializeField] GameObject prefabAttackSFX;
    [SerializeField] GameObject prefabHurtSFX;
    [SerializeField] GameObject prefabDeathSFX;

    AudioSource jumpSFX;
    AudioSource dashSFX;
    AudioSource attackSFX;
    AudioSource hurtSFX;
    AudioSource deathSFX;

    void Start()
    {
        jumpSFX = GetAudioSourceInstance(prefabJumpSFX);
        dashSFX = GetAudioSourceInstance(prefabDashSFX);
        attackSFX = GetAudioSourceInstance(prefabAttackSFX);
        hurtSFX = GetAudioSourceInstance(prefabHurtSFX);
        deathSFX = GetAudioSourceInstance(prefabDeathSFX);
    }

    AudioSource GetAudioSourceInstance(GameObject prefab)
    {
        var instanciaSFX = Instantiate(prefab, transform);
        return instanciaSFX.GetComponent<AudioSource>();
    }

    public void PlayJumpSFX()
    {
        jumpSFX.Play();    
    }

    public void PlayDashSFX()
    {
        dashSFX.Play();
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
