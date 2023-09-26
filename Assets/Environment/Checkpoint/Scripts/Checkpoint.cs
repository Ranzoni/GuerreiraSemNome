using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] CheckpointManager manager;
    [SerializeField] GameObject flagActivated;
    [SerializeField] GameObject flagNotActivated;
    [SerializeField] GameObject prefabSFX;

    bool activated;
    AudioSource sfx;

    void Start()
    {
        flagActivated.SetActive(false);
        sfx = Instantiate(prefabSFX, transform).GetComponent<AudioSource>();
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

        sfx.Play();
    }
}
