using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerManager), typeof(PlayerAnimation), typeof(Rigidbody2D))]
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

    public bool IsAttacking { get { return isAttacking; } }
    public bool IsDashingAttack { get { return isDashingAttack; } }

    bool isAttacking;
    bool isDashingAttack;
    Coroutine checkDashAttackCoroutine;
    bool dashAttackReadyToUse;
    PlayerManager manager;
    PlayerAnimation playerAnimation;
    Rigidbody2D rb2D;

    void Start()
    {
        DisableWeaponAttack();
        manager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (manager.HaveToStopAttack())
        {
            DisableWeaponAttack();
            return;
        }

        ProcessForceMotion();

        if (!manager.CanAttack())
            return;

        if (!Input.GetButtonDown("Fire1") || isAttacking || isDashingAttack)
            return;
        
        StartCoroutine(Attack());
    }

    void ProcessForceMotion()
    {
        if (!manager.IsMoving && checkDashAttackCoroutine is not null)
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

    void FixedUpdate()
    {
        if (isDashingAttack)
            DashAttack();
    }

    void DashAttack()
    {
        var direction = manager.IsFlipped ? -1 : 1;
        var xDirection = direction * dashSpeed;
        var newPosition = new Vector2(xDirection, rb2D.velocity.y);
        rb2D.velocity = newPosition;
    }

    IEnumerator Attack()
    {
        if (dashAttackReadyToUse)
        {
            playerAnimation.TriggerDashAttack();
            isDashingAttack = true;
        }
        else
        {
            playerAnimation.TriggerAttack();
            isAttacking = true;
        }

        yield return new WaitForSeconds(attackDelay);

        isDashingAttack = false;
        isAttacking = false;
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
