using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BringerOfDeathAnimation))]
public class BringerOfDeathAttack : MonoBehaviour
{
    [Tooltip("Tempo em que realizará o ataque")]
    [SerializeField] float attackDelay = 1f;
    [Tooltip("Prefab da arma")]
    [SerializeField] GameObject weapon;

    BringerOfDeathAnimation animator;
    bool isAttacking;

    void Start()
    {
        animator = GetComponent<BringerOfDeathAnimation>();

        DisableWeaponAttack();
    }
    
    public void DisableWeaponAttack()
    {
        weapon.SetActive(false);
    }

    public void EnableWeaponAttack()
    {
        weapon.SetActive(true);
    }

    public void MeleeAttack()
    {
        if (!isAttacking)
            StartCoroutine(MeleeAttackRoutine());
    }

    IEnumerator MeleeAttackRoutine()
    {
        isAttacking = true;
        animator.TriggerMeleeAttack();

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }

    public void SpellAttack()
    {
        if (!isAttacking)
            StartCoroutine(SpellAttackRoutine());
    }

    IEnumerator SpellAttackRoutine()
    {
        isAttacking = true;
        animator.TriggerSpellAttack();

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }
}
