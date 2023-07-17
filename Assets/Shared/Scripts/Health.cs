using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
public class Health : MonoBehaviour
{
    [Tooltip("Quantidade de vida inicial")]
    [SerializeField] int health = 8;
    [SerializeField] float delayHurt = 1f;

    bool isHurting;
    Coroutine hurtCoroutine;
    CharacterAnimation characterAnimation;

    void Start()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    public void TakeDamage(int damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        
        if (isHurting)
            StopCoroutine(hurtCoroutine);
        
        hurtCoroutine = StartCoroutine(HurtRoutine());

        if (IsDead())
            characterAnimation.TriggerDeath();
    }

    IEnumerator HurtRoutine()
    {
        isHurting = true;
        characterAnimation.TriggerHurt();
       
        yield return new WaitForSeconds(delayHurt);

        isHurting = false;
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public bool IsHurting()
    {
        return isHurting;
    }
}
