using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AnimationBringerOfDeath))]
public class TeleportBringerOfDeath : MonoBehaviour
{
    [SerializeField] float delayBackTeleport = 1f;

    public bool IsTeleporting { get { return isTeleporting; } }

    bool isTeleporting;
    AnimationBringerOfDeath animator;

    void Start()
    {
        animator = GetComponent<AnimationBringerOfDeath>();    
    }

    public void Execute()
    {
        if (isTeleporting)
            return;

        animator.TriggerTeleport();
        isTeleporting = true;
    }

    public void Back()
    {
        if (!isTeleporting)
            return;

        animator.TriggerTeleport();
        StartCoroutine(BackTeleportRoutine());
    }

    IEnumerator BackTeleportRoutine()
    {
        yield return new WaitForSeconds(delayBackTeleport);

        isTeleporting = false;
    }
}
