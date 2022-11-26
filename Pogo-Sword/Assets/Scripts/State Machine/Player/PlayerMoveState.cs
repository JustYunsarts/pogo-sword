using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : AbstractPlayerState
{
    public override void EnterState(PlayerStateMachine context)
    {

    }
    public override void UpdateState(PlayerStateMachine context)
    {
        context.flip();

        if (context.horizontalMovment == 0)
        {
            context.SwitchState(context.IdleState);
        }
        else if (context.jumpDown)
        {
            context.SwitchState(context.JumpState);
        }
    }

    public override void FixedUpdateState(PlayerStateMachine context)
    {
        context.RB.velocity = (new Vector2 (context.horizontalMovment * context.walkSpeed, context.RB.velocity.y));
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
