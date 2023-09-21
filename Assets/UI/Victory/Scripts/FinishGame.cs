using System.Collections;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] Health bossHealth;
    [SerializeField] ControlSection section;

    void Update()
    {
        if (bossHealth.IsDead())
            StartCoroutine(FinishGameRoutine());
    }

    IEnumerator FinishGameRoutine()
    {
        yield return new WaitForSeconds(delay);

        section.FinishGame();
    }
}
