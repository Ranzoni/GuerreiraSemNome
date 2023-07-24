using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
public class BanditAttack : MonoBehaviour
{
    [Tooltip("Tempo em que o inimigo realizará o ataque")]
    [SerializeField] float attackDelay = 1f;
    [Tooltip("Prefab da arma")]
    [SerializeField] GameObject weapon;

    CharacterAnimation characterAnimation;
    bool isAttacking;

    void Start()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        DisableWeaponAttack();
    }

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        characterAnimation.TriggerAttack();

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
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
