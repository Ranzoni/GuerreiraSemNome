using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
public class Health : MonoBehaviour
{
    [Tooltip("Quantidade de vida inicial")]
    [SerializeField] int health = 8;
    [Tooltip("Tempo de duração do impacto do dano")]
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
        {
            if (CompareTag("Player"))
            {
                var gameOver = FindObjectOfType<GameOver>();
                gameOver.ExecuteGameOver();
            }

            characterAnimation.TriggerDeath();
        }
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
