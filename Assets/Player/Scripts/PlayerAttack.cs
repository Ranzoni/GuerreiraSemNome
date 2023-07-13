using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Fire1"))
            Attack();
    }

    void Attack()
    {
        Debug.Log("Attacking!");
    }
}
