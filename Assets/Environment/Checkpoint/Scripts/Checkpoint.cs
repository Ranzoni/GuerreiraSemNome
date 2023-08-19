using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] CheckpointManager manager;
    [SerializeField] GameObject flagActivated;
    [SerializeField] GameObject flagNotActivated;

    bool activated;

    void Start()
    {
        flagActivated.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") || activated)
            return;

        activated = true;
        flagActivated.SetActive(true);
        flagNotActivated.SetActive(false);

        var currentPlayerPosition = other.gameObject.transform.position;
        manager.Save(currentPlayerPosition);
    }
}
