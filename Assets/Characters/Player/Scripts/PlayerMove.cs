using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerAttack), typeof(Health))]
public class PlayerMove : MonoBehaviour
{
    [Tooltip("Velocidade do movimento")]
    [SerializeField] float speed = 2f;
    [Tooltip("Altura máxima alcançada com o pulo")]
    [SerializeField] float jumpHeight = 5f;
    [Tooltip("Tempo para acionar o impulso para o ataque de velocidade")]
    [SerializeField] float delayForceMotion = 1f;
    [Tooltip("Velocidade da esquiva")]
    [SerializeField] float dashSpeed = 8f;
    [Tooltip("Tempo da duração da esquiva")]
    [SerializeField] float delayDash = .5f;

    Rigidbody2D rb2D;
    float horizontalMove;
    bool isFlipped;
    bool jumpTriggered;
    bool isJumping;
    bool isDashing;
    bool isForceMotion;
    Coroutine forceMotionCoroutine;
    PlayerAnimation playerAnimation;
    PlayerAttack attack;
    Health health;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        attack = GetComponent<PlayerAttack>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (attack.IsAttacking() || health.IsHurting() || health.IsDead())
        {
            horizontalMove = 0;
            return;
        }

        if (isDashing)
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
        {
            horizontalMove = ReturnTruncatedHorizontalInput();
            Debug.Log(horizontalMove);
            if (Input.GetButtonDown("Fire2") && horizontalMove != 0)
                StartCoroutine(TriggerDash());
        }
    }

    int ReturnTruncatedHorizontalInput()
    {
        var getAxisRaw = Input.GetAxisRaw("Horizontal");
        if (getAxisRaw > 0)
            return 1;
        else if (getAxisRaw < 0)
            return -1;
        else
            return 0;
    }

    IEnumerator TriggerDash()
    {
        isDashing = true;
        playerAnimation.TriggerDash();

        yield return new WaitForSeconds(delayDash);

        isDashing = false;
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
        ProcessForceMotion();

        playerAnimation.SetRun(horizontalMove != 0);

        var velocity = isDashing ? dashSpeed : speed;
        var newPosition = new Vector2(horizontalMove * velocity, rb2D.velocity.y);
        rb2D.velocity = newPosition;
    }

    void ProcessForceMotion()
    {
        if (horizontalMove == 0 && forceMotionCoroutine is not null)
        {
            isForceMotion = false;
            StopCoroutine(forceMotionCoroutine);
            forceMotionCoroutine = null;
        }
        else if (forceMotionCoroutine is null)
            forceMotionCoroutine = StartCoroutine(TriggerForceMotion());
    }

    IEnumerator TriggerForceMotion()
    {
        yield return new WaitForSeconds(delayForceMotion);

        isForceMotion = true;
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

    public bool IsForceMotion()
    {
        return isForceMotion;
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
