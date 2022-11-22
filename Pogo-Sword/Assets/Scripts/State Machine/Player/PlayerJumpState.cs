using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : AbstractPlayerState
{
    private float velocityToAdd;
    private bool jumped;
    public override void EnterState(PlayerStateMachine context)
    {
        jumped = false;
        context.RB.gravityScale = context.jumpGravity;
        velocityToAdd = context.jumpForce;
    }
    public override void UpdateState(PlayerStateMachine context)
    {
        context.flip();

        if (context.jumpUp || (context.RB.velocity.y <= 0) && jumped)
        {
            context.RB.velocity = new Vector2(context.RB.velocity.x, context.RB.velocity.y * 0.5f);
            context.SwitchState(context.FallState);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            context.SwitchState(context.PogoState);
        }

    }

    public override void FixedUpdateState(PlayerStateMachine context)
    {
        if (jumped == false)
        {
            context.RB.velocity =
                (new Vector2(context.horizontalMovment * context.walkSpeed, velocityToAdd));
            jumped = true;
        }
        else
        {
            context.RB.velocity =
                (new Vector2(context.horizontalMovment * context.walkSpeed, context.RB.velocity.y));
        }
    }
    public override void OnTriggerEnter2D(PlayerStateMachine context, Collider2D collision)
    {
    }

    public override void OnCollisionEnter2D(PlayerStateMachine context, Collision2D collision)
    {

    }

}
