using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMove), typeof(Health))]
public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Intervalo (em segundos) para acionar um novo ataque após um ataque padrão")]
    [SerializeField] float attackDelay = 1f;
    [Tooltip("Velocidade do ataque de impulso")]
    [SerializeField] float dashSpeed = 4f;
    [Tooltip("Prefab da arma")]
    [SerializeField] GameObject weapon;
    [Tooltip("Tempo para liberar o ataque de velocidade (ele é contado enquanto o jogador estiver em movimento)")]
    [SerializeField] float delayForceMotion = 1f;

    PlayerAnimation playerAnimation;
    PlayerMove move;
    Health health;
    bool isAttacking;
    bool isDashingAttack;
    Coroutine checkDashAttackCoroutine;
    bool dashAttackReadyToUse;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        move = GetComponent<PlayerMove>();
        health = GetComponent<Health>();
        DisableWeaponAttack();
    }

    void Update()
    {
        if (health.IsHurting() || health.IsDead())
        {
            DisableWeaponAttack();
            return;
        }

        ProcessForceMotion();

        if (move.IsDashing() || move.IsJumping())
            return;

        if (isDashingAttack)
        {
            DashAttack();
            return;
        }

        if (!Input.GetButtonDown("Fire1") || isAttacking || move.IsJumping())
            return;
        
        StartCoroutine(Attack());
    }

    void ProcessForceMotion()
    {
        if (!move.IsMoving() && checkDashAttackCoroutine is not null)
        {
            dashAttackReadyToUse = false;
            StopCoroutine(checkDashAttackCoroutine);
            checkDashAttackCoroutine = null;
        }
        else if (checkDashAttackCoroutine is null)
            checkDashAttackCoroutine = StartCoroutine(CheckDashAttackRoutine());
    }

    IEnumerator CheckDashAttackRoutine()
    {
        yield return new WaitForSeconds(delayForceMotion);

        dashAttackReadyToUse = true;
    }

    void DashAttack()
    {
        var direction = move.IsFlipped() ? Vector2.left : Vector2.right;
        var nextPosition = direction * dashSpeed * Time.deltaTime;
        transform.Translate(nextPosition);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        move.StopRun();
        if (dashAttackReadyToUse)
        {
            playerAnimation.TriggerDashAttack();
            isDashingAttack = true;
        }
        else
            playerAnimation.TriggerAttack();

        yield return new WaitForSeconds(attackDelay);

        isDashingAttack = false;
        isAttacking = false;
    }
    
    public bool IsAttacking()
    {
        return isAttacking || isDashingAttack;
    }

    public void EnableWeaponAttack()
    {
        weapon.SetActive(true);
    }

    public void DisableWeaponAttack()
    {
        weapon.SetActive(false);
    }
}
