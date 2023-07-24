using UnityEngine;

public class BringerOfDeathAI : MonoBehaviour
{
    [Tooltip("Raio de visão que irá encadiar a perseguição ao alvo")]
    [SerializeField] float rangeOfVision = 10f;
    [Tooltip("Raio de visão que irá encadiar o ataque corpo-a-corpo ao alvo")]
    [SerializeField] float rangeToFollow = 5f;
    [Tooltip("A distância mínima entre o inimigo e o alvo na perseguição")]
    [SerializeField] float minimumDistance = 2f;

    GameObject target;
    EnemyMove move;

    void Start()
    {
        target = FindObjectOfType<PlayerMove>().gameObject;
        move = GetComponent<EnemyMove>();
    }

    void Update()
    {
        move.StopMove();

        var centerPosition = GetCenterPosition(transform);
        var targetCenterPosition = GetCenterPosition(target.transform);
        var targetDistance = Vector2.Distance(centerPosition, targetCenterPosition);

        if (targetDistance > rangeOfVision)
            return;
            
        if (targetDistance <= rangeToFollow)
            ProcessMelee(targetDistance, targetCenterPosition);
        else
            SpellAttack();
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
            MeleeAttack(targetCenterPosition);
    }

    void MeleeAttack(Vector2 position)
    {
        move.Flip(position);
        // attack.Attack();
        Debug.Log("Ataque corpo-a-corpo");
    }

    void SpellAttack()
    {
        Debug.Log("Ataque de magia");
    }
}
