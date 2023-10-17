using System.Collections;
using UnityEngine;

public class HintControl : MonoBehaviour
{
    [SerializeField] float delayToHide = 10f;

    void Start()
    {
        StartCoroutine(HideHintRoutine());
    }

    IEnumerator HideHintRoutine()
    {
        yield return new WaitForSeconds(delayToHide);

        gameObject.SetActive(false);
    }
}
