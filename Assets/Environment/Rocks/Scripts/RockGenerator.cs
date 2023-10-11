using System.Collections;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [SerializeField] float delayGeneration = .5f;
    [Tooltip("Prefabs das pedras que serão geradas")]
    [SerializeField] GameObject[] listPrefabRock;
    [SerializeField] Transform[] listPositions;

    bool started;
    bool finished;
    AudioSource sfx;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    IEnumerator GenerateRandomPrefabRoutine()
    {
        while (true)
        {
            if (finished)
                break;

            var indexPosition = Random.Range(0, listPositions.Length);
            var xValue = listPositions[indexPosition].position.x;
            var rockPosition = new Vector2(xValue, transform.position.y);

            var prefab = ReturnRandomPrefab();
            var rock = Instantiate(prefab, rockPosition, Quaternion.identity);

            rock.transform.parent = transform;
            
            yield return new WaitForSeconds(delayGeneration);
        }
    }

    GameObject ReturnRandomPrefab()
    {
        while (true)
        {
            var index = Random.Range(0, listPrefabRock.Length);

            return listPrefabRock[index];
        };
    }

    public void StartRockGeneration()
    {
        if (started)
            return;

        sfx.Play();
        started = true;
        StartCoroutine(GenerateRandomPrefabRoutine());
    }

    public void FinishRockGeneration()
    {
        if (!started)
            return;

        finished = true;
    }
}
