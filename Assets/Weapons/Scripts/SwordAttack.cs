using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Debug.Log("Damage");
    }
}
