using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject sword;

    PlayerAnimation playerAnimation;

    bool hasAttack;

    void Start()
    {
        sword.SetActive(false);
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
