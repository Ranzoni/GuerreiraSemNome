using UnityEngine;

public class GroundCollision : MonoBehaviour
{
    [Tooltip("Pontos de colisão do chão")]
    [SerializeField] Transform[] groundChecks;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] ControlLadder controlLadder;

    public bool IsFalling { get { return isFalling; } }

    bool isFalling;
    bool isGrounded;
    bool lastStatus;

    void Start()
    {
        lastStatus = !isGrounded;
    }

    void Update()
    {
        isGrounded = CheckGrounded();
    }

    void FixedUpdate()
    {
        if (isGrounded == lastStatus)
            return;

        if (controlLadder.IsLadding)
            return;

        if (isGrounded && playerMove.IsJumping)
            playerMove.StopJump();

        Fall(!isGrounded);

        lastStatus = isGrounded;
    }

    void Fall(bool active)
    {
        playerAnimation.SetFall(active);
        isFalling = active;
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
