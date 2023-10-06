using System.Collections;
using UnityEngine;

public class ClearRock : MonoBehaviour
{
    [SerializeField] float delayToClear = 5f;

    void Start()
    {
        StartCoroutine(ClearRoutine());
    }

    IEnumerator ClearRoutine()
    {
        yield return new WaitForSeconds(delayToClear);

        Destroy(gameObject);
    }
}
