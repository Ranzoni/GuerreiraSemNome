using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationBringerOfDeath))]
public class AttackBringerOfDeath : MonoBehaviour
{
    [Tooltip("Tempo em que realizará o ataque")]
    [SerializeField] float meleeAttackDelay = 1f;
    [Tooltip("Tempo em que realizará o ataque")]
    [SerializeField] float spellAttackDelay = 2f;
    [Tooltip("Prefab da arma")]
    [SerializeField] GameObject weapon;
    [Tooltip("Prefab da magia")]
    [SerializeField] GameObject spell;

    public bool IsAttacking { get { return isAttacking; } }

    AnimationBringerOfDeath animator;
    bool isAttacking;

    void Start()
    {
        animator = GetComponent<AnimationBringerOfDeath>();
        spell.SetActive(false);

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

        yield return new WaitForSeconds(meleeAttackDelay);

        isAttacking = false;
    }

    public void SpellAttack(Vector2 targetPosition)
    {
        if (!isAttacking)
        {
            spell.transform.position = new Vector2(targetPosition.x, spell.transform.position.y);
            StartCoroutine(SpellAttackRoutine());
        }
    }

    IEnumerator SpellAttackRoutine()
    {
        isAttacking = true;
        animator.TriggerSpellAttack();
        spell.SetActive(true);

        yield return new WaitForSeconds(spellAttackDelay);

        spell.SetActive(false);
        isAttacking = false;
    }
}
