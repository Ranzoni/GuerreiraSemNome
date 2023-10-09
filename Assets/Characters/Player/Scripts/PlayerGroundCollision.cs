using UnityEngine;

[RequireComponent(typeof(PlayerManager), typeof(PlayerAnimation))]
public class PlayerGroundCollision : MonoBehaviour
{
    [Tooltip("Pontos de colisão do chão")]
    [SerializeField] Transform[] groundChecks;
    [SerializeField] float fallLimit = 6f;

    public bool IsFalling { get { return isFalling; } }

    bool isFalling;
    bool isGrounded;
    bool lastStatus;
    float yStartFall;
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
        if (manager.IsOnStairs)
        {
            Debug.Log("IsOnStairs");
            yStartFall = 0;
            return;
        }

        if (isGrounded == lastStatus)
            return;

        if (isGrounded && manager.IsJumping)
            manager.StopJump();

        Fall(!isGrounded);

        lastStatus = isGrounded;
    }

    void Fall(bool statusIsFalling)
    {
        if (statusIsFalling)
        {
            yStartFall = transform.position.y;
        }
        else if (yStartFall != 0)
        {
            var disanceFromStarFall = yStartFall - transform.position.y;
            if (disanceFromStarFall > fallLimit)
                SendMessage("FallDamage");

            yStartFall = 0;
        } 

        playerAnimation.SetFall(statusIsFalling);
        isFalling = statusIsFalling;
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
