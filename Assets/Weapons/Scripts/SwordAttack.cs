using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [SerializeField] int damage = 2;

    Health health;

    void Start()
    {
        health = GetComponentInParent<Health>();
    }

    void Update() {
        if (health is not null && health.IsDead())
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.CompareTag("Enemy") || other.CompareTag("Player"))) 
            return;

        if (transform.parent.CompareTag("Enemy") && other.CompareTag("Enemy"))
            return;            

        var health = other.GetComponent<Health>();
        if (health is not null)
            health.TakeDamage(damage);
    }
}
