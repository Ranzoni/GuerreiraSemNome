using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Intervalo (em segundos) para acionar um novo ataque")]
    [SerializeField] float attackDelay = 1f;

    PlayerAnimation playerAnimation;
    bool attackTriggered;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!attackTriggered)
                StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        attackTriggered = true;
        playerAnimation.TriggerAttack();

        yield return new WaitForSeconds(attackDelay);

        attackTriggered = false;
    }
}
