using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MoveBringerOfDeath), typeof(AttackBringerOfDeath))]
public class AIBringerOfDeath : MonoBehaviour
{
    [Tooltip("Raio de visão que irá encadiar o ataque corpo-a-corpo ao alvo")]
    [SerializeField] float rangeToFollow = 5f;
    [Tooltip("A distância mínima entre o inimigo e o alvo na perseguição")]
    [SerializeField] float minimumDistance = 2f;
    [Tooltip("Tempo para destruir o inimigo após dele ser morto")]
    [SerializeField] float timeToDestroyAfterDeath = 1f;

    GameObject target;
    MoveBringerOfDeath move;
    AttackBringerOfDeath attack;
    Health health;
    Health targetHealth;
    bool processDeath;

    void Start()
    {
        target = FindObjectOfType<PlayerMove>().gameObject;
        move = GetComponent<MoveBringerOfDeath>();
        attack = GetComponent<AttackBringerOfDeath>();
        health = GetComponent<Health>();
        targetHealth = target.GetComponent<Health>();
    }

    void Update()
    {
        move.StopMove();
        if (health.IsDead())
        {
            if (!processDeath)
                StartCoroutine(DestroyRoutine());

            return;
        }

        if (health.IsHurting() || targetHealth.IsDead() || attack.IsAttacking)
            return;

        var targetCenterPosition = GetCenterPosition(target.transform);
        var targetDistance = Vector2.Distance(transform.position, targetCenterPosition);
            
        if (targetDistance <= rangeToFollow)
            ProcessMelee(targetDistance, targetCenterPosition);
        else
            SpellAttack();
    }

    IEnumerator DestroyRoutine()
    {
        processDeath = true;

        yield return new WaitForSeconds(timeToDestroyAfterDeath);

        Destroy(gameObject);
    }

    Vector2 GetCenterPosition(Transform transform)
    {
        var centerValueX = transform.position.x - (transform.localScale.x / 2);
        return new Vector2(centerValueX, transform.position.y);
    }

    void ProcessMelee(float targetDistance, Vector2 targetCenterPosition)
    {
        if (targetDistance > minimumDistance)
            move.Move(targetCenterPosition);
        else
            attack.MeleeAttack();
    }

    void SpellAttack()
    {
        attack.SpellAttack(target.transform.position);
    }
}
