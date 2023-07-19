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

    PlayerAnimation playerAnimation;
    PlayerMove move;
    Health health;
    bool isAttacking;
    bool isDashingAttack;

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

    IEnumerator Attack()
    {
        isAttacking = true;
        var isForceMotion = move.IsForceMotion();
        move.StopRun();
        if (isForceMotion)
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

    void DashAttack()
    {
        var direction = move.IsFlipped() ? Vector2.left : Vector2.right;
        var nextPosition = direction * dashSpeed * Time.deltaTime;
        transform.Translate(nextPosition);
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
