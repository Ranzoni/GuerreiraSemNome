using Cinemachine;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField] GameOver gameOver;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        gameOver.ExecuteGameOver();

        var cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (cinemachineVirtualCamera is not null)
            FindObjectOfType<CinemachineVirtualCamera>().enabled = false;
    }
}
