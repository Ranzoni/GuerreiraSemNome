using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMove))]
public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Intervalo (em segundos) para acionar um novo ataque após um ataque padrão")]
    [SerializeField] float attackDelay = 1f;
    [Tooltip("Intervalo (em segundos) para acionar um novo ataque de velocidade")]
    [SerializeField] float dashAttackDelay = .5f;
    [SerializeField] float dashSpeed = 4f;
    [SerializeField] GameObject sword;

    PlayerAnimation playerAnimation;
    bool isAttacking;
    bool isDashingAttack;
    PlayerMove move;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        move = GetComponent<PlayerMove>();
    }

    void Update()
    {
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
        var isDashing = move.IsDashing();
        move.StopRun();
        if (isDashing)
        {
            playerAnimation.TriggerDashAttack();
            isDashingAttack = true;
        }
        else
            playerAnimation.TriggerAttack();

        yield return new WaitForSeconds(DelayToAnotherAttack());

        isDashingAttack = false;
        isAttacking = false;
    }

    float DelayToAnotherAttack()
    {
        return isDashingAttack ? dashAttackDelay : attackDelay;
    }

    void DashAttack()
    {
        var direction = move.IsFlipped() ? Vector2.left : Vector2.right;
        var nextPosition = direction * dashSpeed * Time.deltaTime;
        transform.Translate(nextPosition);
    }
    
    public bool IsAttacking()
    {
        return isAttacking;
    }

    public void EnableWeaponAttack()
    {
        sword.SetActive(true);
    }

    public void DisableWeaponAttack()
    {
        sword.SetActive(false);
    }
}
