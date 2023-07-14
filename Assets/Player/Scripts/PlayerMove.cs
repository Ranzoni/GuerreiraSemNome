using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimation), typeof(PlayerAttack))]
public class PlayerMove : MonoBehaviour
{
    [Tooltip("Velocidade do movimento")]
    [SerializeField] float velocity = 2f;
    [Tooltip("Altura máxima alcançada com o pulo")]
    [SerializeField] float jumpHeight = 5f;

    Rigidbody2D rb2D;
    float horizontalMove;
    bool isFlipped;
    bool jumpTriggered;
    bool isJumping;
    PlayerAnimation playerAnimation;
    PlayerAttack attack;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        attack = GetComponent<PlayerAttack>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
    }

    void Update()
    {
        if (attack.IsAttacking())
            return;

        ProcessRun();
        ProcessJump();
        Flip();
    }

    void ProcessRun()
    {
        if (attack.IsAttacking())
            horizontalMove = 0;
        else
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
            isFlipped = !isFlipped;
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
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

    public bool IsJumping()
    {
        return jumpTriggered || isJumping;
    }

    public void StopRun()
    {
        horizontalMove = 0;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        isJumping = false;
    }
}
