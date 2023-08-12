using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookWallCollision : MonoBehaviour
{
    [Tooltip("Ponto de colisão na lateral")]
    [SerializeField] Transform hookWallCheck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckHookWall()
    {
        var direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        var hit = Physics2D.Raycast(hookWallCheck.position, direction, 0.2f);
        var isCollidingHookWall = hit.collider != null && hit.collider.gameObject.CompareTag("HookWall");

        return isCollidingHookWall;
    }
}
