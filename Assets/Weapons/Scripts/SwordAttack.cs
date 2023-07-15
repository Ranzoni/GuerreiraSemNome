using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] int damage = 2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;
        
        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth is not null)
            enemyHealth.TakeDamage(damage);
    }
}
