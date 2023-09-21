using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] Health bossHealth;
    [SerializeField] ControlSection section;

    void Update()
    {
        if (bossHealth.IsDead())
            section.FinishGame();            
    }
}
