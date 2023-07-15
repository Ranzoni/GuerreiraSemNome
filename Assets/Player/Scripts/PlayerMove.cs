using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerAnimation), typeof(PlayerAttack))]
public class PlayerMove : MonoBehaviour
{
    [Tooltip("Velocidade do movimento")]
    [SerializeField] float velocity = 2f;
    [Tooltip("Altura máxima alcançada com o pulo")]
    [SerializeField] float jumpHeight = 5f;
    [Tooltip("Tempo para acionar o impulso")]
    [SerializeField] float delayDash = 1f;

    Rigidbody2D rb2D;
    float horizontalMove;
    bool isFlipped;
    bool jumpTriggered;
    bool isJumping;
    bool isDashing;
    Coroutine dashCoroutine;
    PlayerAnimation playerAnimation;
    PlayerAttack attack;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        attack = GetComponent<PlayerAttack>();
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
            var xPosition = transform.position.x;
            localScale.x *= -1;
            xPosition += localScale.x;
            transform.position = new Vector2(xPosition, transform.position.y);
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
        ProcessDash();

        playerAnimation.SetRun(horizontalMove != 0);

        var newPosition = new Vector2(horizontalMove * velocity, rb2D.velocity.y);
        rb2D.velocity = newPosition;
    }

    void ProcessDash()
    {
        if (horizontalMove == 0 && dashCoroutine is not null)
        {
            isDashing = false;
            StopCoroutine(dashCoroutine);
            dashCoroutine = null;
        }
        else if (dashCoroutine is null)
            dashCoroutine = StartCoroutine(TriggerDash());
    }

    IEnumerator TriggerDash()
    {
        yield return new WaitForSeconds(delayDash);

        isDashing = true;
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

    public bool IsFlipped()
    {
        return isFlipped;
    }

    public bool IsJumping()
    {
        return jumpTriggered || isJumping;
    }

    public void StopRun()
    {
        horizontalMove = 0;
    }

    public bool IsDashing()
    {
        return isDashing;
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        isJumping = false;
    }
}
