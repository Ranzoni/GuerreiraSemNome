using UnityEngine;

[RequireComponent(typeof(PlayerManager), typeof(PlayerAnimation))]
public class GroundCollision : MonoBehaviour
{
    [Tooltip("Pontos de colisão do chão")]
    [SerializeField] Transform[] groundChecks;

    public bool IsFalling { get { return isFalling; } }

    bool isFalling;
    bool isGrounded;
    bool lastStatus;
    PlayerManager manager;
    PlayerAnimation playerAnimation;

    void Start()
    {
        lastStatus = !isGrounded;
        manager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        isGrounded = CheckGrounded();
    }

    void FixedUpdate()
    {
        if (isGrounded == lastStatus)
            return;

        if (manager.IsOnStairs)
            return;

        if (isGrounded && manager.IsJumping)
            manager.StopJump();

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
