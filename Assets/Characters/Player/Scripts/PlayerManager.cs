using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] PlayerMove move;
    [SerializeField] PlayerAttack attack;
    [SerializeField] Health health;
    [SerializeField] PlayerClimbStairsControl climbStairsControl;
    [SerializeField] GroundCollision groundCollision;

    public bool IsMoving { get { return move.IsMoving; } }
    public bool IsFlipped { get { return move.IsFlipped; } }
    public bool IsJumping { get { return move.IsJumping; } }
    public bool IsOnStairs { get { return climbStairsControl.IsTheStairs; } }

    public void StopRun()
    {
        move.StopRun();
    }

    public void StopJump()
    {
        move.StopJump();
    }

    public bool CanAttack()
    {
        return !(move.IsDashing
            || move.IsJumping
            || climbStairsControl.IsTheStairs
            || groundCollision.IsFalling);
    }

    public bool HaveToStopAttack()
    {
        return health.IsHurting || health.IsDead();
    }

    public bool HaveToStopMovement()
    {
        return attack.IsAttacking || health.IsHurting || health.IsDead();
    }

    public bool CanDash()
    {
        return move.IsMoving && !move.IsDashing && !HaveToStopMovement();
    }

    public bool CanJump()
    {
        return !groundCollision.IsFalling || climbStairsControl.IsTheStairs;
    }

    public bool CanMove()
    {
        return !climbStairsControl.IsTheStairs;
    }

    public bool CanGoStairs()
    {
        return !(move.IsMoving || attack.IsAttacking) || groundCollision.IsFalling;
    }
}
