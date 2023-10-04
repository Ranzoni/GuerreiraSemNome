using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] CheckpointManager manager;
    [SerializeField] GameObject flagActivated;
    [SerializeField] GameObject flagNotActivated;
    [SerializeField] GameObject prefabSFX;

    AudioSource sfx;
    BoxCollider2D bc2D;

    void Start()
    {
        flagActivated.SetActive(false);
        sfx = Instantiate(prefabSFX, transform).GetComponent<AudioSource>();
        bc2D = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        flagActivated.SetActive(true);
        flagNotActivated.SetActive(false);

        var currentPlayerPosition = other.gameObject.transform.position;
        manager.Save(currentPlayerPosition);

        sfx.Play();

        bc2D.enabled = false;
    }
}
