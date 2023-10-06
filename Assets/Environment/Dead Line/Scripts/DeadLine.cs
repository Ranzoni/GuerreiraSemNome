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

        InactivePlayer(other.gameObject);
    }

    void InactivePlayer(GameObject player)
    {
        var playerStatusManager = player.GetComponent<PlayerControlStatus>();
        if (playerStatusManager is not null)
            playerStatusManager.Inative();
    }
}
