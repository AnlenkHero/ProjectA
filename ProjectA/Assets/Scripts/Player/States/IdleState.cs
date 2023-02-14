using UnityEngine;

public class IdleState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetBool("IsIdle",true);
        Debug.Log("Entering Idle State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.moveVector.magnitude != 0)
        {
            player.SwitchState(player.RunningState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.animator.SetBool("IsIdle",false);
        Debug.Log("Exiting Idle State");
    }
}
