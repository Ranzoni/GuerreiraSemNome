using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : CharacterAnimation
{
    public void SetJump(bool active)
    {
        animator.SetBool("jump", active);
    }

    public void SetFall(bool active)
    {
        animator.SetBool("fall", active);
    }

    public void TriggerDashAttack()
    {
        animator.SetTrigger("dashAttack");
    }

    public void TriggerDash()
    {
        animator.SetTrigger("dash");
    }

    public void SetGrab(bool active)
    {
        animator.SetBool("grabbing", active);
    }
}
