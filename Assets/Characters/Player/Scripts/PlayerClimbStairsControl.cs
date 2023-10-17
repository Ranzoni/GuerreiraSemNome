using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerManager), typeof(PlayerAnimation))]
public class PlayerClimbStairsControl : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] TilemapCollider2D groundCollider;

    public bool IsTheStairs { get { return isOnTheStair; } }

    GameObject stairs;
    bool isOnTheStair;
    Rigidbody2D rb2D;
    float gravityScale;
    PlayerManager manager;
    PlayerAnimation playerAnimation;

    void Start()
    {
        manager = GetComponent<PlayerManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        rb2D = GetComponent<Rigidbody2D>();
        gravityScale = rb2D.gravityScale;
    }

    void Update()
    {
        if (manager.IsDead)
        {
            GetOutStairs();
            return;
        }

        if (manager.IsHurting)
        {
            playerAnimation.ContinueAnimation();
            return;
        }

        if (!manager.CanGoStairs())
            return;

        if (stairs is null || Input.GetButtonDown("Jump"))
        {
            if (stairs is null)
                ActiveGround(true);

            GetOutStairs();
            return;
        }

        if (Input.GetAxisRaw("Vertical") == 0)
        {
            if (isOnTheStair)
                playerAnimation.PauseAnimation();

            return;
        }

        GoOnStairs();
    }

    void GetOutStairs()
    {
        if (!isOnTheStair)
            return;

        rb2D.gravityScale = gravityScale;
        playerAnimation.ContinueAnimation();
        isOnTheStair = false;
        playerAnimation.SetMoveOnStairs(false);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    void GoOnStairs()
    {
        if (!isOnTheStair)
        {
            isOnTheStair = true;
            playerAnimation.SetMoveOnStairs(true);
            rb2D.gravityScale = 0;
            rb2D.bodyType = RigidbodyType2D.Kinematic;
            ActiveGround(false);
        }

        manager.StopJump();

        rb2D.velocity = new Vector2(0, 0);
        playerAnimation.ContinueAnimation();
        var position = Input.GetAxisRaw("Vertical") < 0 ? Vector2.down : Vector2.up;
        transform.Translate(position * speed * Time.deltaTime);
    }

    void ActiveGround(bool active)
    {
        groundCollider.enabled = active;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stairs") && stairs is null)
            stairs = other.gameObject;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (stairs is not null && stairs.Equals(other.gameObject))
            stairs = null;
    }
}
