using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MoveBringerOfDeath), typeof(AttackBringerOfDeath), typeof(TeleportBringerOfDeath))]
public class AIBringerOfDeath : MonoBehaviour
{
    [Tooltip("Raio de visão que irá encadiar a perseguição ao alvo")]
    [SerializeField] float rangeToFollow = 5f;
    [Tooltip("Raio de visão que irá encadiar o ataque corpo-a-corpo ao alvo")]
    [SerializeField] float rangeToAttack = 2f;
    [Tooltip("A distância mínima entre o inimigo e o alvo na perseguição")]
    [SerializeField] float minimumDistance = .5f;
    [Tooltip("Tempo para destruir o inimigo após dele ser morto")]
    [SerializeField] float delayDestroy = 1f;
    [Tooltip("Quantidade de golpes para encadiar o teleporte")]
    [SerializeField] int hitsToTeleport = 2;
    [Tooltip("Tempo para destruir o inimigo após dele ser morto")]
    [SerializeField] float delayTeleport = 2f;
    [Tooltip("Ponto inicial para poder se teleportar")]
    [SerializeField] Transform startTransformTeleport;
    [Tooltip("Ponto final para poder se teleportar")]
    [SerializeField] Transform endTransformTeleport;
    [SerializeField] Transform transformBOD;

    GameObject target;
    MoveBringerOfDeath move;
    AttackBringerOfDeath attack;
    Health health;
    TeleportBringerOfDeath teleport;
    Health targetHealth;
    bool processDeath;
    int hitsTake;

    void Start()
    {
        target = FindObjectOfType<PlayerMove>().gameObject;
        move = GetComponent<MoveBringerOfDeath>();
        attack = GetComponent<AttackBringerOfDeath>();
        health = GetComponent<Health>();
        teleport = GetComponent<TeleportBringerOfDeath>();
        targetHealth = target.GetComponent<Health>();
    }

    void Update()
    {
        move.StopMove();
        if (IsDead())
            return;

        ProcessTeleport();

        if (!CanTakeAction())
        {
            attack.DisableWeaponAttack();
            return;
        }

        if (attack.IsAttacking)
            return;

        var targetCenterPosition = GetCenterPosition(target.transform);
        var targetDistance = Vector2.Distance(transformBOD.position, targetCenterPosition);
        move.Flip(targetCenterPosition);
            
        if (targetDistance <= rangeToFollow)
            ProcessMelee(targetDistance, targetCenterPosition);
        else
            SpellAttack();
    }

    bool IsDead()
    {
        if (health.IsDead())
        {
            if (!processDeath)
                StartCoroutine(DestroyRoutine());

            attack.DisableWeaponAttack();
            return true;
        }

        return false;
    }

    IEnumerator DestroyRoutine()
    {
        processDeath = true;

        yield return new WaitForSeconds(delayDestroy);

        Destroy(gameObject);
    }

    void ProcessTeleport()
    {
        if (hitsTake >= hitsToTeleport)
        {
            hitsTake = 0;
            StartCoroutine(TeleportRoutine());
        }
        
        health.IsInvincible = teleport.IsTeleporting;
    }

    IEnumerator TeleportRoutine()
    {
        teleport.Execute();

        yield return new WaitForSeconds(delayTeleport);

        transform.position = PositionToTeleportFound();
        move.Flip(target.transform.position);
        teleport.Back();
    }

    Vector2 PositionToTeleportFound()
    {
        var starterX = startTransformTeleport.position.x;
        var endedX = endTransformTeleport.position.x;
        var targetCenterPosition = GetCenterPosition(target.transform);
        var lastCost = float.MinValue;
        var cheapestPosition = transform.position;

        for (var x = starterX; x <= endedX; x++)
        {
            var cost = 0f;
            if ((target.transform.localScale.x > 0 && x < targetCenterPosition.x) || (target.transform.localScale.x < 0 && x > targetCenterPosition.x))
                cost = 1f;

            var position = new Vector2(x, transform.position.y);
            cost += Vector2.Distance(targetCenterPosition, position);
            if (cost > lastCost)
            {
                lastCost = cost;
                cheapestPosition = position;
            }
        }

        return cheapestPosition;
    }

    bool CanTakeAction()
    {
        return !health.IsHurting && !targetHealth.IsDead() && !teleport.IsTeleporting;
    }

    Vector2 GetCenterPosition(Transform transform)
    {
        var centerValueX = transform.position.x - (transform.localScale.x / 2);
        return new Vector2(centerValueX, 0);
    }

    void ProcessMelee(float targetDistance, Vector2 targetCenterPosition)
    {
        if (targetDistance > rangeToAttack)
            move.Move(targetCenterPosition);
        else if (targetDistance < minimumDistance && !teleport.IsTeleporting)
            StartCoroutine(TeleportRoutine());
        else
            attack.MeleeAttack();
    }

    void SpellAttack()
    {
        attack.SpellAttack(target.transform.position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerWeapon") && health.IsHurting && !teleport.IsTeleporting)
            hitsTake += 1;
    }
}
