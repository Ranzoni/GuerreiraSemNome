using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
public class BringerOfDeathMove : MonoBehaviour
{
    [Tooltip("Velocidade de movimentação")]
    [SerializeField] float velocity = 2f;
    
    CapsuleCollider2D objCollider2D;
    bool isFlipped;
    CharacterAnimation characterAnimation;

    void Start()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        objCollider2D = GetComponent<CapsuleCollider2D>();
    }

    public void Move(Vector2 direction)
    {
        Flip(direction);
        Walk(direction);
    }

    public void Flip(Vector2 direction)
    {
        if (direction.x > transform.position.x && !isFlipped || direction.x < transform.position.x && isFlipped)
        {
            isFlipped = !isFlipped;
            var localScale = transform.localScale;
            localScale.x *= -1;
            var xPosition = transform.position.x - (objCollider2D.size.x + Mathf.Abs(localScale.x)) * localScale.x;
            transform.position = new Vector2(xPosition, transform.position.y);
            transform.localScale = localScale;
        }
    }

    void Walk(Vector2 direction)
    {
        var directionXOnly = new Vector2(direction.x, transform.position.y);
        characterAnimation.SetRun(transform.position.x != directionXOnly.x);

        transform.position = Vector2.MoveTowards(transform.position, directionXOnly, velocity * Time.deltaTime);
    }

    public void StopMove()
    {
        characterAnimation.SetRun(false);
    }
}
