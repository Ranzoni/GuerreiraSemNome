using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimation))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float velocity = 2f;
    [SerializeField] float jumpHeight = 5f;

    Rigidbody2D rb2D;
    float horizontalMove;
    bool isFlipped;
    bool jumpTriggered;
    bool isJumping;
    PlayerAnimation playerAnimation;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        ProcessRun();
        ProcessJump();
        Flip();
    }

    void ProcessRun()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
    }

    void ProcessJump()
    {
        jumpTriggered = Input.GetButton("Jump");
    }

    void Flip()
    {
        if (horizontalMove < 0 && !isFlipped || horizontalMove > 0 && isFlipped)
        {
            playerAnimation.gameObject.SetActive(false);
            isFlipped = !isFlipped;
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            playerAnimation.gameObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Run();
        Jump();
    }

    void Run()
    {
        playerAnimation.SetRun(horizontalMove != 0);

        var newPosition = new Vector2(horizontalMove * velocity, rb2D.velocity.y);
        rb2D.velocity = newPosition;
    }

    void Jump()
    {
        if (!jumpTriggered || isJumping)
            return;

        playerAnimation.TriggerJump();
        var newPosition = new Vector2(rb2D.velocity.x, jumpHeight);
        rb2D.velocity = newPosition;
        isJumping = true;
        jumpTriggered = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        isJumping = false;
    }
}
