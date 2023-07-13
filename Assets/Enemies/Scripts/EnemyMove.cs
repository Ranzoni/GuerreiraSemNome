using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float velocity = 2f;

    bool isFlipped;
    // PlayerAnimation playerAnimation;

    void Start()
    {
        // playerAnimation = GetComponent<PlayerAnimation>();
    }

    public void Move(Vector2 direction)
    {
        Flip(direction);
        
        Run(direction);
    }

    void Flip(Vector2 direction)
    {
        if (direction.x > transform.position.x && !isFlipped || direction.x < transform.position.x && isFlipped)
        {
            // playerAnimation.gameObject.SetActive(false);
            isFlipped = !isFlipped;
            var localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
            // playerAnimation.gameObject.SetActive(true);
        }
    }

    void Run(Vector2 direction)
    {
        // playerAnimation.SetRun(horizontalMove != 0);
        var directionXOnly = new Vector2(direction.x, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, directionXOnly, velocity * Time.deltaTime);
    }
}
