using UnityEngine;

public class RunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetBool("IsRun",true);
        Debug.Log("Entering Run State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.moveVector.magnitude == 0)
        {
            player.SwitchState(player.IdlingState);
        }
        else
        {
            player.Move();
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.animator.SetBool("IsRun",false);
        Debug.Log("Exiting Run State");
    }
}
