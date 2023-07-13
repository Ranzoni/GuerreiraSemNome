using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health = 8;

    void TakeDamage(int damage)
    {
        health -= damage;
    }
}
