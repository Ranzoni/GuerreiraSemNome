using UnityEngine;

[RequireComponent(typeof(EnemyMove), typeof(EnemyAttack), typeof(EnemyHealth))]
public class EnemyAI : MonoBehaviour
{
    [Tooltip("Alvo que irá seguir e atacar")]
    [SerializeField] GameObject target;
    [Tooltip("Raio de visão que irá encadiar a perseguição ao alvo")]
    [SerializeField] float rangeFollow = 10f;
    [Tooltip("A distância mínima entre o inimigo e o alvo na perseguição")]
    [SerializeField] float minimumDistance = 2f;

    EnemyMove move;
    EnemyAttack attack;
    EnemyHealth health;

    void Start()
    {
        move = GetComponent<EnemyMove>();
        attack = GetComponent<EnemyAttack>();
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        move.StopMove();
        if (health.IsDead() || health.IsHurting())
            return;

        var targetCenterPosition = GetCenterPosition(target.transform);
        var targetDistance = Vector2.Distance(transform.position, targetCenterPosition);

        if (targetDistance <= rangeFollow)
        {
            if (targetDistance > minimumDistance)
            {
                if (!attack.IsAttacking())
                    move.Move(targetCenterPosition);
            }
            else
            {
                move.Flip(targetCenterPosition);
                attack.Attack();
            }
        }
    }

    Vector2 GetCenterPosition(Transform transform)
    {
        var centerValueX = transform.position.x - (transform.localScale.x / 2);
        return new Vector2(centerValueX, transform.position.y);
    }
}
