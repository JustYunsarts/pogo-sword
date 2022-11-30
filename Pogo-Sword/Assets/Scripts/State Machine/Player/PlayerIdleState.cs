using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : AbstractPlayerState
{
    //play idle animation
    //if you detect any movement, then move to the appropriate state:
    //Moving state for A and D
    //Jumping State for SPACE
    public override void EnterState(PlayerStateMachine context)
    {

    }
    public override void UpdateState(PlayerStateMachine context)
    {
        if (context.horizontalMovment != 0)
        {
            context.SwitchState(context.MoveState);
        }

        else if (context.jumpDown)
        {
            context.SwitchState(context.JumpState);
        }


        if (!context.IsGrounded())
        {
            context.SwitchState(context.FallState);
        }

    }

    public override void FixedUpdateState(PlayerStateMachine context)
    {
        context.RB.velocity = (new Vector2(0, context.RB.velocity.y));
    }

    public override void OnTriggerEnter2D(PlayerStateMachine context, Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            context.SwitchState(context.DeathState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine context, Collision2D collision)
    {

    }

}
