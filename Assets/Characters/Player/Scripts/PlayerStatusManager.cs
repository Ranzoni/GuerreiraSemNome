using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerStatusManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    Health health;
    Rigidbody2D rb2D;

    void Start()
    {
        health = GetComponent<Health>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void Inative()
    {
        cinemachineVirtualCamera.enabled = false;
    }

    public void ResetStatus(Vector2 position)
    {
        rb2D.velocity = new Vector2(0, 0);
        cinemachineVirtualCamera.enabled = true;
        health.Restore();
        transform.position = position;
    }
}
