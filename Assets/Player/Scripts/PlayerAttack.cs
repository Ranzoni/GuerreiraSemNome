using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerAttack : MonoBehaviour
{
    PlayerAnimation playerAnimation;

    bool hasAttack;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Attack();
    }

    void Attack()
    {
        playerAnimation.TriggerAttack();
    }
}
