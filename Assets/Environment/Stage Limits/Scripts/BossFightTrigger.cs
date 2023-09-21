using System.Collections;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] Transform boss;
    [SerializeField] Transform startLimit;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Player"))
            return;

        StartCoroutine(StartBossFightRoutine());
    }

    IEnumerator StartBossFightRoutine()
    {
        yield return new WaitForSeconds(delay);
        startLimit.gameObject.SetActive(true);
        boss.gameObject.SetActive(true);
    }
}
