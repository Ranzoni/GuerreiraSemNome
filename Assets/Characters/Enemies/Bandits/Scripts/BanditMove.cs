using UnityEngine;

[RequireComponent(typeof(CharacterAnimation))]
public class BanditMove : MonoBehaviour
{
    [Tooltip("Velocidade de movimentação")]
    [SerializeField] float velocity = 2f;

    bool isFlipped;
    CharacterAnimation enemyAnimation;

    void Start()
    {
        enemyAnimation = GetComponent<CharacterAnimation>();
    }

    public void Move(Vector2 direction)
    {
        Flip(direction);
        Run(direction);
    }

    public void Flip(Vector2 direction)
    {
        if (direction.x > transform.position.x && !isFlipped || direction.x < transform.position.x && isFlipped)
        {
            isFlipped = !isFlipped;
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    void Run(Vector2 direction)
    {
        var directionXOnly = new Vector2(direction.x, transform.position.y);
        enemyAnimation.SetRun(transform.position.x != directionXOnly.x);

        transform.position = Vector2.MoveTowards(transform.position, directionXOnly, velocity * Time.deltaTime);
    }

    public void StopMove()
    {
        enemyAnimation.SetRun(false);
    }
}
