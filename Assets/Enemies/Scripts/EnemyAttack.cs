using UnityEngine;

[RequireComponent(typeof(EnemyAnimation))]
public class EnemyAttack : MonoBehaviour
{
    EnemyAnimation enemyAnimation;

    void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    public void Attack()
    {
        enemyAnimation.TriggerAttack();
    }
}
