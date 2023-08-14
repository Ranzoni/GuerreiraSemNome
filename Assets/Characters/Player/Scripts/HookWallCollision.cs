using UnityEngine;

public class HookWallCollision : MonoBehaviour
{
    [Tooltip("Ponto de colisão na lateral")]
    [SerializeField] Transform hookWallCheck;

    PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    void FixedUpdate()
    {
        var isHookingWall = CheckHookWall();

        if (isHookingWall)
        {
            playerMove.StopJump();
            playerMove.SetGrab(true);
        }

    }

    bool CheckHookWall()
    {
        var direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        var hit = Physics2D.Raycast(hookWallCheck.position, direction, 0.2f);
        var isCollidingHookWall = hit.collider != null && hit.collider.gameObject.CompareTag("HookWall");

        return isCollidingHookWall;
    }
}
