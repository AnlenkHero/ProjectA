using UnityEngine;

public class FallState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Fall State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.controller.isGrounded)
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
        Debug.Log("Exiting Fall State");
    }
}
