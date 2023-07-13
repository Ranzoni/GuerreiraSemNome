using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float velocity = 2f;
    [SerializeField] float jumpHeight = 5f;

    Rigidbody2D rb2D;
    float horizontalMove;
    bool isFlipped;
    bool jumpTriggered;
    bool isJumping;
    Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        RunAnimation();

        var newPosition = new Vector2(horizontalMove * velocity, rb2D.velocity.y);
        rb2D.velocity = newPosition;
    }

    void RunAnimation()
    {
        animator.SetBool("move", horizontalMove != 0);
    }

    void Jump()
    {
        if (!jumpTriggered || isJumping)
            return;

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
