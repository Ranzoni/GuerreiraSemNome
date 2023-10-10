using System.Collections;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [SerializeField] float delayGeneration = .5f;
    [Tooltip("Prefabs das pedras que serão geradas")]
    [SerializeField] GameObject[] listPrefabRock;
    [SerializeField] Transform[] listPositions;

    bool started;

    IEnumerator GenerateRandomPrefabRoutine()
    {
        while (true)
        {
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

        started = true;
        StartCoroutine(GenerateRandomPrefabRoutine());
    }
}
