using UnityEngine;

public class FinishRockGeneration : MonoBehaviour
{
    [SerializeField] RockGenerator rockGenerator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.tag.Equals("Player"))
            return;

        rockGenerator.FinishRockGeneration();        
    }
}
