using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();   
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        playerMove.StopJump();
        playerMove.SetFall(false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        playerMove.SetFall(true);
    }
}
