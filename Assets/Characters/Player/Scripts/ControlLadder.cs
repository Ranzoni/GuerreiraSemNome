using UnityEngine;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerMove), typeof(PlayerAttack))]
public class ControlLadder : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [Tooltip("Pontos de colisão")]
    [SerializeField] Transform[] stairChecks;

    public bool IsLadding { get { return isLadding; } }

    GameObject stairs;
    PlayerAnimation playerAnimation;
    bool isLadding;
    Rigidbody2D rb2D;
    PlayerMove playerMove;
    PlayerAttack playerAttack;

    void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        return;
        
        if (stairs is null || Input.GetButtonDown("Jump") || playerMove.IsMoving)
        {
            StopLandding();
            return;
        }

        if (Input.GetAxisRaw("Vertical") == 0)
        {
            if (isLadding)
                playerAnimation.PauseAnimation();

            return;
        }

        StartLandding();
    }

    void StartLandding()
    {
        if (!isLadding)
        {
            isLadding = true;
            playerAnimation.SetLadder(true);
            rb2D.bodyType = RigidbodyType2D.Kinematic;
        }

        playerAnimation.ContinueAnimation();
        var position = Input.GetAxisRaw("Vertical") < 0 ? Vector2.down : Vector2.up;
        transform.Translate(position * speed * Time.deltaTime);
    }

    void StopLandding()
    {
        if (!isLadding)
            return;

        playerAnimation.ContinueAnimation();
        isLadding = false;
        playerAnimation.SetLadder(false);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Stairs"))
    //         stairs = other.gameObject;
    // }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if (stairs == other.gameObject)
    //         stairs = null;
    // }

    void FixedUpdate()
    {
        CheckStairs();
    }

    void CheckStairs()
    {
        foreach (Transform stairCheck in stairChecks)
        {
            var point = transform.localScale.x < 1 ? Vector2.left : Vector2.right;
            var hit = Physics2D.Raycast(stairCheck.position, point, .75f);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Stairs"))
            {
                stairs = hit.collider.gameObject;
                return;
            }

            stairs = null;
        }
    }
}
