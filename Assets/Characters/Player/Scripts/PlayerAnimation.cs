using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : CharacterAnimation
{
    public void TriggerJump()
    {
        animator.SetTrigger("jump");
    }

    public void TriggerDashAttack()
    {
        animator.SetTrigger("dashAttack");
    }
}
