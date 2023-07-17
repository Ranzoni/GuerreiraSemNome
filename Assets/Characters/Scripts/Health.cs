using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
public class Health : MonoBehaviour
{
    [Tooltip("Quantidade de vida inicial")]
    [SerializeField] int health = 8;
    [Tooltip("Tempo de duração do impacto do dano")]
    [SerializeField] float delayHurt = 1f;

    public int MaxHealth { get { return maxHealth; } }
    public int HealthAmount { get { return health; } }

    bool isHurting;
    Coroutine hurtCoroutine;
    CharacterAnimation characterAnimation;
    int maxHealth;

    void Start()
    {
        maxHealth = health;
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    public void TakeDamage(int damage)
    {
        if (IsDead())
            return;

        health -= damage;
        
        ProcessHurt();
        ProcessDeath();
    }

    void ProcessHurt()
    {
        if (isHurting)
            StopCoroutine(hurtCoroutine);
        
        hurtCoroutine = StartCoroutine(HurtRoutine());
    }

    IEnumerator HurtRoutine()
    {
        isHurting = true;
        characterAnimation.TriggerHurt();
       
        yield return new WaitForSeconds(delayHurt);

        isHurting = false;
    }

    void ProcessDeath()
    {
        if (!IsDead())
            return;

        if (CompareTag("Player"))
        {
            var gameOver = FindObjectOfType<GameOver>();
            gameOver.ExecuteGameOver();
        }

        characterAnimation.TriggerDeath();
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
