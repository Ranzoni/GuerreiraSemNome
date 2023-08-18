using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] CheckpointManager manager;

    bool activated;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || activated)
            return;

        activated = true;
        manager.SetPosition(other.gameObject.transform.position);
    }
}
