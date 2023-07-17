using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
}
