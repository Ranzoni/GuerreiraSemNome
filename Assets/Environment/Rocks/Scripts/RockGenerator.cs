using System.Collections;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [SerializeField] float delayGeneration = .5f;
    [SerializeField] int maxRange = 10;
    [Tooltip("Prefabs das pedras que serão geradas")]
    [SerializeField] GameObject[] listPrefabRock;

    bool started;

    IEnumerator GenerateRandomPrefabRoutine()
    {
        while (true)
        {
            var xValue = Random.Range(transform.position.x, transform.position.x + maxRange);
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

        started = true;
        StartCoroutine(GenerateRandomPrefabRoutine());
    }
}
