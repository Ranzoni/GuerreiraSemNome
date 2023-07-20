using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    [Tooltip("Pontos de colisão do chão")]
    [SerializeField] Transform[] groundChecks;
    [Tooltip("Layer de checagem do chão")]
    [SerializeField] LayerMask groundLayer;
    
    PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }    

    private void FixedUpdate()
    {
        var isGrounded = CheckGrounded();

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
