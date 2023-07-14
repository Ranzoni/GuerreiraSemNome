using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMove))]
public class PlayerAttack : MonoBehaviour
{
    [Tooltip("Intervalo (em segundos) para acionar um novo ataque")]
    [SerializeField] float attackDelay = 1f;

    PlayerAnimation playerAnimation;
    bool isAttacking;
    PlayerMove move;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        move = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if (!Input.GetButtonDown("Fire1") || isAttacking || move.IsJumping())
            return;
        
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        move.StopRun();
        playerAnimation.TriggerAttack();

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }
    
    public bool IsAttacking()
    {
        return isAttacking;
    }
}
