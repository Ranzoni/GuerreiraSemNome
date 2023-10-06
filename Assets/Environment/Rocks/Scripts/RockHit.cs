using UnityEngine;

public class RockHit : MonoBehaviour
{
    [SerializeField] int damage = 4;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Player"))
            return;

        var playerHealth = other.gameObject.GetComponent<Health>();
        playerHealth.TakeDamage(damage);
    }
}
