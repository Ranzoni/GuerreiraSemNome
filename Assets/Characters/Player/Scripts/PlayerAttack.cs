using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerManager), typeof(PlayerAnimation))]
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

    public bool IsAttacking { get { return isAttacking || isDashingAttack ; } }

    bool isAttacking;
    bool isDashingAttack;
    Coroutine checkDashAttackCoroutine;
    bool dashAttackReadyToUse;
    PlayerManager manager;
    PlayerAnimation playerAnimation;

    void Start()
    {
        DisableWeaponAttack();
        manager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (manager.BreakAttack())
        {
            DisableWeaponAttack();
            return;
        }

        ProcessForceMotion();

        if (!manager.CanAttack())
            return;

        if (isDashingAttack)
        {
            DashAttack();
            return;
        }

        if (!Input.GetButtonDown("Fire1") || isAttacking)
            return;
        
        StartCoroutine(Attack());
    }

    void ProcessForceMotion()
    {
        if (!manager.IsMoving() && checkDashAttackCoroutine is not null)
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
        var direction = manager.IsFlipped() ? Vector2.left : Vector2.right;
        var nextPosition = direction * dashSpeed * Time.deltaTime;
        transform.Translate(nextPosition);
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        manager.StopRun();
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

    public void EnableWeaponAttack()
    {
        weapon.SetActive(true);
    }

    public void DisableWeaponAttack()
    {
        weapon.SetActive(false);
    }
}
