using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimation))]
public class EnemyAttack : MonoBehaviour
{
    [Tooltip("Tempo em que o inimigo realizará o ataque")]
    [SerializeField] float attackDelay = 1f;
    [Tooltip("Prefab da arma")]
    [SerializeField] GameObject weapon;

    EnemyAnimation enemyAnimation;
    bool isAttacking;

    void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
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
        enemyAnimation.TriggerAttack();

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
