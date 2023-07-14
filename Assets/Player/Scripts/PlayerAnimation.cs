using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetRun(bool active)
    {
        animator.SetBool("move", active);
    }

    public void TriggerJump()
    {
        animator.SetTrigger("jump");
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("attack");
    }

    public void TriggerDashAttack()
    {
        animator.SetTrigger("dashAttack");
    }
}
