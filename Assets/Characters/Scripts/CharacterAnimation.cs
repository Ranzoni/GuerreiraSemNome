using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;
    public float speedAnimation;

    void Start()
    {
        animator = GetComponent<Animator>();
        speedAnimation = animator.speed;
    }

    public void SetRun(bool active)
    {
        animator.SetBool("move", active);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("attack");
    }

    public void TriggerHurt()
    {
        animator.SetTrigger("hurt");
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("death");
    }

    public void TriggerTeleport()
    {
        animator.SetTrigger("teleport");
    }
}
