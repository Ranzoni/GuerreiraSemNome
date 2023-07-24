using UnityEngine;

[RequireComponent(typeof(BanditMove), typeof(BanditAttack), typeof(Health))]
public class BanditAI : MonoBehaviour
{
    [Tooltip("Raio de visão que irá encadiar a perseguição ao alvo")]
    [SerializeField] float rangeFollow = 10f;
    [Tooltip("A distância mínima entre o inimigo e o alvo na perseguição")]
    [SerializeField] float minimumDistance = 2f;

    GameObject target;
    BanditMove move;
    BanditAttack attack;
    Health health;
    Health targetHealth;

    void Start()
    {
        target = FindObjectOfType<PlayerMove>().gameObject;
        move = GetComponent<BanditMove>();
        attack = GetComponent<BanditAttack>();
        health = GetComponent<Health>();
        targetHealth = target.GetComponent<Health>();
    }

    void Update()
    {
        move.StopMove();
        if (health.IsDead() || health.IsHurting() || targetHealth.IsDead())
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
