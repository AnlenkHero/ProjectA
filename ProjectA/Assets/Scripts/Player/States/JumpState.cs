using UnityEngine;

public class JumpState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.animator.SetBool("IsJump",true);
        Debug.Log("Entering Jump State");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (player.isGrounded)
        {
            player.playerVelocity.y += Mathf.Sqrt(player.jumpHeight * -3.0f * player.gravityValue);
            player.Move();
        }
        else if (!player.isGrounded && player.CurrentState == player.JumpingState)
        {
            player.SwitchState(player.FallingState);
        }
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.animator.SetBool("IsJump",false);
        Debug.Log("Exiting Jump State");
    }
}
