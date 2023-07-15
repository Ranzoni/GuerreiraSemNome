using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimation))]
public class EnemyHealth : MonoBehaviour
{
    [Tooltip("Quantidade de vida inicial")]
    [SerializeField] int health = 8;
    [SerializeField] float delayHurt = 1f;

    bool isHurting;
    Coroutine hurtCoroutine;
    EnemyAnimation enemyAnimation;

    void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
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
            enemyAnimation.TriggerDeath();
    }

    IEnumerator HurtRoutine()
    {
        isHurting = true;
        enemyAnimation.TriggerHurt();
       
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
