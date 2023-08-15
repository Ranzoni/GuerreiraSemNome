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

    public void SetLadder(bool active)
    {
        animator.SetBool("ladder", active);
    }

    public void PauseAnimation()
    {
        if (animator.speed == 0)
            return;
            
        speedAnimation = animator.speed;
        animator.speed = 0;
    }

    public void ContinueAnimation()
    {
        animator.speed = speedAnimation;
    }
}
