using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (!other.gameObject.CompareTag("Ground"))
    //         return;

    //     playerMove.StopJump();
    //     playerMove.SetFall(false);
    // }

    // void OnCollisionExit2D(Collision2D other)
    // {
    //     if (!other.gameObject.CompareTag("Ground"))
    //         return;

    //     playerMove.SetFall(true);
    // }

    public Transform[] groundChecks;
    public LayerMask groundLayer;

    bool isGrounded = false;

    private void FixedUpdate()
    {
        // Verifica se algum dos pontos groundChecks está tocando o chão (Layer "Ground")
        isGrounded = CheckGrounded();

        if (isGrounded)
            playerMove.StopJump();

        playerMove.SetFall(!isGrounded);
    }

    bool CheckGrounded()
    {
        foreach (Transform groundCheck in groundChecks)
        {
            var hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
                return true;
        }
        return false;
    }
}
