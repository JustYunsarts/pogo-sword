using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : AbstractPlayerState
{
    bool isGrounded;
    float coyoteTimeCounter;
    public override void EnterState(PlayerStateMachine context)
    {
        context.RB.gravityScale = context.gravityScale;
        coyoteTimeCounter = 0f;
    }
    public override void UpdateState(PlayerStateMachine context)
    {
        context.flip();
        isGrounded = Physics2D.OverlapCircle(context.feetPos.position, context.feetRadius, context.groundChecker);
        if (isGrounded)
        {
            coyoteTimeCounter = context.coyoteTime;
        }

        if (!isGrounded)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (context.horizontalMovment == 0)
        {
            context.SwitchState(context.IdleState);
        }
        if (context.jumpDown && coyoteTimeCounter > 0)
        {
            context.SwitchState(context.JumpState);
        }
        else if (coyoteTimeCounter < 0)
        {
            context.SwitchState(context.FallState);
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
