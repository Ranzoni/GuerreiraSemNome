using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
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

    public void TriggerAttack()
    {
        animator.SetTrigger("attack");
    }
}