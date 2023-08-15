using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ControlLadder : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] PlayerMove playerMove;
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] GroundCollision groundCollision;

    public bool IsLadding { get { return isLadding; } }

    GameObject stairs;
    bool isLadding;
    Rigidbody2D rb2D;
    float gravityScale;
    bool startStair;
    bool endStair;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        gravityScale = rb2D.gravityScale;
    }

    void Update()
    {
        if (!(playerMove.IsMoving || playerAttack.IsAttacking) || groundCollision.IsFalling)
        {
            if (stairs is null || Input.GetButtonDown("Jump"))
            {
                StopLadding();
                return;
            }

            if (Input.GetAxisRaw("Vertical") == 0)
            {
                if (isLadding)
                    playerAnimation.PauseAnimation();

                return;
            }

            StartLadding();
        }
    }

    public void StopLadding()
    {
        if (!isLadding)
            return;

        rb2D.gravityScale = gravityScale;
        playerAnimation.ContinueAnimation();
        isLadding = false;
        playerAnimation.SetLadder(false);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    void StartLadding()
    {
        if (!FinishedStairs())
        {
            StopLadding();
            return;
        }

        if (!isLadding)
        {
            isLadding = true;
            playerAnimation.SetLadder(true);
            rb2D.gravityScale = 0;
            rb2D.bodyType = RigidbodyType2D.Kinematic;
        }

        rb2D.velocity = new Vector2(0, 0);
        playerAnimation.ContinueAnimation();
        var position = Input.GetAxisRaw("Vertical") < 0 ? Vector2.down : Vector2.up;
        transform.Translate(position * speed * Time.deltaTime);
    }

    bool FinishedStairs()
    {
        if (Input.GetAxisRaw("Vertical") > 0 && endStair)
            return false;

        if (Input.GetAxisRaw("Vertical") < 0 && startStair)
            return false;

        return true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Stairs") && stairs is null)
            stairs = other.gameObject;

        startStair = other.gameObject.CompareTag("StartStair");
        endStair = other.gameObject.CompareTag("EndStair");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (stairs is not null && stairs.Equals(other.gameObject))
            stairs = null;
    }
}
