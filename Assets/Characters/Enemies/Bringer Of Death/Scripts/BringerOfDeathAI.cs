using UnityEngine;

public class BringerOfDeathAI : MonoBehaviour
{
    [Tooltip("Raio de visão que irá encadiar o ataque corpo-a-corpo ao alvo")]
    [SerializeField] float rangeToFollow = 5f;
    [Tooltip("A distância mínima entre o inimigo e o alvo na perseguição")]
    [SerializeField] float minimumDistance = 2f;

    GameObject target;
    BringerOfDeathMove move;
    BringerOfDeathAttack attack;

    void Start()
    {
        target = FindObjectOfType<PlayerMove>().gameObject;
        move = GetComponent<BringerOfDeathMove>();
        attack = GetComponent<BringerOfDeathAttack>();
    }

    void Update()
    {
        move.StopMove();

        var targetCenterPosition = GetCenterPosition(target.transform);
        var targetDistance = Vector2.Distance(transform.position, targetCenterPosition);
            
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
            attack.MeleeAttack();
    }

    void SpellAttack()
    {
        attack.SpellAttack(target.transform.position);
    }
}
