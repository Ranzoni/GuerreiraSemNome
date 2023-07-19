using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : CharacterAnimation
{
    public void SetJump(bool active)
    {
        animator.SetBool("jump", active);
    }

    public void TriggerFall()
    {
        animator.SetTrigger("fall");
    }

    public void TriggerStopFall()
    {
        animator.SetTrigger("stopFall");
    }

    public void TriggerDashAttack()
    {
        animator.SetTrigger("dashAttack");
    }

    public void TriggerDash()
    {
        animator.SetTrigger("dash");
    }
}
