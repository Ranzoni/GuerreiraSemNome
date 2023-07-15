using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMove))]
public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Intervalo (em segundos) para acionar um novo ataque após um ataque padrão")]
    [SerializeField] float attackDelay = 1f;
    [Tooltip("Intervalo (em segundos) para acionar um novo ataque de impulso")]
    [SerializeField] float dashAttackDelay = .5f;
    [Tooltip("Velocidade do ataque de impulso")]
    [SerializeField] float dashSpeed = 4f;
    [Tooltip("Prefab da arma")]
    [SerializeField] GameObject weapon;

    PlayerAnimation playerAnimation;
    PlayerMove move;
    bool isAttacking;
    bool isDashingAttack;

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
        weapon.SetActive(true);
    }

    public void DisableWeaponAttack()
    {
        weapon.SetActive(false);
    }
}
