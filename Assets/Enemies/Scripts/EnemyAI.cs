using UnityEngine;

[RequireComponent(typeof(EnemyMove), typeof(EnemyAttack))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float rangeFollow = 10f;
    [SerializeField] float minimumDistance = 2f;

    EnemyMove move;
    EnemyAttack attack;

    void Start()
    {
        move = GetComponent<EnemyMove>();
        attack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        move.StopMove();
        var targetDistance = Vector2.Distance(transform.position, target.transform.position);

        if (targetDistance <= rangeFollow)
        {
            if (targetDistance > minimumDistance)
                move.Move(target.transform.position);
            else
            {
                move.Flip(target.transform.position);
                attack.Attack();
            }
        }
    }
}
