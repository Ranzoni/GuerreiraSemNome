using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMove move;
    [SerializeField] Health health;
    [SerializeField] ControlLadder ladder;
    [SerializeField] GroundCollision groundCollision;

    public bool IsMoving()
    {
        return move.IsMoving;
    }

    public bool IsFlipped()
    {
        return move.IsFlipped;
    }

    public void StopRun()
    {
        move.StopRun();
    }

    public bool CanAttack()
    {
        return !(move.IsDashing
            || move.IsJumping
            || ladder.IsLadding
            || groundCollision.IsFalling);
    }

    public bool BreakAttack()
    {
        return health.IsHurting || health.IsDead();
    }
}
