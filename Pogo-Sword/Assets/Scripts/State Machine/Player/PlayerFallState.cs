using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : AbstractPlayerState
{
    bool isGrounded;
    public override void EnterState(PlayerStateMachine context)
    {
        //Set falling gravity to normal to counteract the change in gravity while jumping, to avoid
        //the physics from feeling floaty
        context.RB.gravityScale = context.gravityScale;
    }
    public override void UpdateState(PlayerStateMachine context)
    {
        context.flip();

        //Draw a circle around the player's feet. If in contact with the ground layer, switch to idle state
        isGrounded = Physics2D.OverlapCircle(context.feetPos.position, context.feetRadius, context.groundChecker);

        if (isGrounded)
        {
            context.SwitchState(context.IdleState);
        }

        //if at any point you press the left shift, you enter pogo state.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            context.SwitchState(context.PogoState);
        }
    }
    public override void FixedUpdateState(PlayerStateMachine context)
    {
        context.RB.velocity =
            (new Vector2(context.horizontalMovment * context.walkSpeed, context.RB.velocity.y));
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
