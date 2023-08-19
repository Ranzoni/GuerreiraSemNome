using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] PlayerStatusManager playerStatusManager;
    [SerializeField] EnemiesPoolControl enemiesPoolControl;

    public bool HasCheckpoint { get { return positionSaved is not null; } }

    Vector2? positionSaved;

    public void Save(Vector2 playerPosition)
    {
        SavePosition(playerPosition);
    }

    void SavePosition(Vector2 position)
    {
        positionSaved = position;
    }

    public void RestoreToCheckpoint()
    {
        if (positionSaved is null)
            return;

        var positionToRespawn = (Vector2)positionSaved;
        playerStatusManager.ResetStatus(positionToRespawn);
        enemiesPoolControl.SetPool();
    }
}
