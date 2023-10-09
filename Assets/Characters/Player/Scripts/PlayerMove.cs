using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerManager), typeof(PlayerAnimation))]
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

    public bool IsMoving { get { return componentRun.HorizontalMove != 0; } }
    public bool IsDashing { get { return componentDash.IsDashing; } }
    public bool IsJumping { get { return componentJump.IsJumping; } }
    public bool IsFlipped { get { return componentFlip.IsFlipped; } }
    
    ComponentRun componentRun;
    ComponentDash componentDash;
    ComponentJump componentJump;
    ComponentFlip componentFlip;
    PlayerManager manager;
    PlayerAnimation playerAnimation;

    #region Classes Components Of PlayerMove

    class ComponentRun
    {
        public float HorizontalMove { get { return horizontalMove; } }

        float horizontalMove;
        readonly PlayerAnimation playerAnimation;
        Rigidbody2D rb2D;

        public ComponentRun(Rigidbody2D rb2D, PlayerAnimation playerAnimation)
        {
            this.rb2D = rb2D;
            this.playerAnimation = playerAnimation;
        }

        public void PopulateHorizontalMove()
        {
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

        public void ExecuteDash(float speed)
        {
            var newPosition = new Vector2(speed, rb2D.velocity.y);
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
        public float HorizontalMove { get { return horizontalMove; } }

        bool isDashing;
        float horizontalMove;
        readonly PlayerAnimation playerAnimation;

        public ComponentDash(PlayerAnimation playerAnimation)
        {
            this.playerAnimation = playerAnimation;
        }

        public void Execute(float horizontalMove)
        {
            this.horizontalMove = horizontalMove;
            isDashing = true;
            playerAnimation.TriggerDash();
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

        public void Stop()
        {
            isJumping = false;
            playerAnimation.SetJump(false);
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
        manager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        var rb2D = GetComponent<Rigidbody2D>();

        componentRun = new ComponentRun(rb2D, playerAnimation);
        componentDash = new ComponentDash(playerAnimation);
        componentJump  = new ComponentJump(rb2D, playerAnimation);
        componentFlip  = new ComponentFlip();

        StartCoroutine(DashRoutine());
    }

    IEnumerator DashRoutine()
    {
        while (true)
        {
            if (Input.GetButton("Fire2") && manager.CanDash())
            {
                componentDash.Execute(componentRun.HorizontalMove);

                yield return new WaitForSeconds(delayDash);

                componentDash.Stop();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    void Update()
    {
        if (manager.HaveToStopMovement())
        {
            componentRun.Stop();
            return;
        }

        if (componentDash.IsDashing || manager.IsDashingAttack)
            return;

        if (manager.CanMove())
            componentRun.PopulateHorizontalMove();

        if (manager.CanJump())
            componentJump.TriggerJump();

        componentFlip.Execute(transform, componentRun.HorizontalMove);
    }

    void FixedUpdate()
    {
        if (manager.CanMove())
        {
            if (componentDash.IsDashing)
                componentRun.ExecuteDash(dashSpeed * componentDash.HorizontalMove);
            else
                componentRun.Execute(speed);
        }
            
        componentJump.Execute(jumpHeight);
    }

    public void StopJump()
    {
        componentJump.Stop();
    }
}
