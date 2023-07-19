using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerAttack), typeof(Health))]
public class PlayerMove : MonoBehaviour
{
    [Tooltip("Velocidade do movimento")]
    [SerializeField] float speed = 2f;
    [Tooltip("Altura máxima alcançada com o pulo")]
    [SerializeField] float jumpHeight = 5f;
    [Tooltip("Velocidade da esquiva")]
    [SerializeField] float dashSpeed = 8f;
    [Tooltip("Tempo da duração da esquiva")]
    [SerializeField] float delayDash = .5f;
    [Tooltip("Objeto que irá checar o ponto de colisão com o chão (Ele deve estar posicionado no ponto que o personagem irá se chocar com o chão)")]
    [SerializeField] GameObject groundPoint;
    [Tooltip("Raio de checagem da colisão com o chão")]
    [SerializeField] float groundRay = .2f;
    
    PlayerAnimation playerAnimation;
    PlayerAttack playerAttack;
    Health health;
    ComponentRun playerRun;
    ComponentDash playerDash;
    ComponentJump playerJump;
    ComponentFlip playerFlip;

    #region Classes Components Of PlayerMove

    class ComponentRun
    {
        public float HorizontalMove { get { return horizontalMove; } }

        float horizontalMove;
        readonly PlayerAttack playerAttack;
        readonly PlayerAnimation playerAnimation;
        Rigidbody2D rb2D;

        public ComponentRun(Rigidbody2D rb2D, PlayerAttack playerAttack, PlayerAnimation playerAnimation)
        {
            this.rb2D = rb2D;
            this.playerAttack = playerAttack;
            this.playerAnimation = playerAnimation;
        }

        public void PopulateHorizontalMove()
        {
            if (playerAttack.IsAttacking())
                horizontalMove = 0;
            else
                horizontalMove = ReturnIntHorizontalAxis();
        }

        int ReturnIntHorizontalAxis()
        {
            var getAxisRaw = Input.GetAxisRaw("Horizontal");
            if (getAxisRaw > 0)
                return 1;
            else if (getAxisRaw < 0)
                return -1;
            else
                return 0;
        }

        public void Execute(float speed)
        {
            playerAnimation.SetRun(horizontalMove != 0);
            
            var newPosition = new Vector2(horizontalMove * speed, rb2D.velocity.y);
            rb2D.velocity = newPosition;
        }

        public void Stop()
        {
            horizontalMove = 0;
        }
    }

    class ComponentDash
    {
        public bool IsDashing { get { return isDashing; } }

        bool isDashing;
        readonly PlayerAnimation playerAnimation;

        public ComponentDash(PlayerAnimation playerAnimation)
        {
            this.playerAnimation = playerAnimation;
        }

        public void Execute(float horizontalMove)
        {
            if (Input.GetButtonDown("Fire2") && horizontalMove != 0)
            {
                isDashing = true;
                playerAnimation.TriggerDash();
            }
        }

        public void Stop()
        {
            isDashing = false;
        }
    }

    class ComponentJump
    {
        public bool IsJumping { get { return isJumping; } }

        bool jumpTriggered;
        bool isJumping;
        Rigidbody2D rb2D;
        readonly PlayerAnimation playerAnimation;

        public ComponentJump(Rigidbody2D rb2D, PlayerAnimation playerAnimation)
        {
            this.playerAnimation = playerAnimation;
            this.rb2D = rb2D;
        }

        public void TriggerJump()
        {
            if (Input.GetButtonDown("Jump") && !isJumping)
                jumpTriggered = true;
        }

        public void Execute(float jumpHeight)
        {
            if (!jumpTriggered || isJumping)
                return;

            playerAnimation.SetJump(true);
            isJumping = true;
            jumpTriggered = false;
            
            rb2D.velocity = Vector2.up * jumpHeight;
        }

        public void StopJumping()
        {
            isJumping = false;
            playerAnimation.SetJump(false);
            playerAnimation.SetFall(false);
        }
    }

    class ComponentFlip
    {
        public bool IsFlipped { get { return isFlipped; } }

        bool isFlipped;

        public void Execute(Transform transform, float horizontalMove)
        {
            if (!(horizontalMove < 0 && !isFlipped || horizontalMove > 0 && isFlipped))
                return;
            
            isFlipped = !isFlipped;
            var localScale = transform.localScale;
            var xPosition = transform.position.x;
            localScale.x *= -1;
            xPosition += localScale.x;
            transform.position = new Vector2(xPosition, transform.position.y);
            transform.localScale = localScale;
        }
    }

    #endregion Classes Components Of PlayerMove

    void Start()
    {
        var rb2D = GetComponent<Rigidbody2D>();
        playerAttack = GetComponent<PlayerAttack>();
        playerAnimation = GetComponent<PlayerAnimation>();

        playerRun = new ComponentRun(rb2D, playerAttack, playerAnimation);
        playerDash = new ComponentDash(playerAnimation);
        playerJump  = new ComponentJump(rb2D, playerAnimation);
        playerFlip  = new ComponentFlip();
        
        health = GetComponent<Health>();
    }

    void Update()
    {
        if (PlayerHasToStop())
        {
            StopRun();
            return;
        }

        if (playerDash.IsDashing)
            return;

        ProcessGroundCollision();
        playerRun.PopulateHorizontalMove();
        StartCoroutine(DashRoutine());
        playerJump.TriggerJump();
        playerFlip.Execute(transform, playerRun.HorizontalMove);
    }

    bool PlayerHasToStop()
    {
        return playerAttack.IsAttacking() || health.IsHurting() || health.IsDead();
    }

    void ProcessGroundCollision()
    {
        var hit = Physics2D.Raycast(groundPoint.transform.position, Vector2.down, groundRay);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
            playerJump.StopJumping();
    }

    IEnumerator DashRoutine()
    {
        playerDash.Execute(playerRun.HorizontalMove);

        yield return new WaitForSeconds(delayDash);

        playerDash.Stop();
    }

    void FixedUpdate()
    {
        var moveSpeed = playerDash.IsDashing ? dashSpeed : speed;
        playerRun.Execute(moveSpeed);
        playerJump.Execute(jumpHeight);
    }

    public bool IsMoving()
    {
        return playerRun.HorizontalMove != 0;
    }

    public bool IsFlipped()
    {
        return playerFlip.IsFlipped;
    }

    public bool IsJumping()
    {
        return playerJump.IsJumping;
    }

    public void StopRun()
    {
        playerRun.Stop();
    }
    
    public bool IsDashing()
    {
        return playerDash.IsDashing;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground"))
            return;

        playerAnimation.SetFall(true);
    }
}
