using UnityEngine;

public class RockMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.timeScale);
    }
}
