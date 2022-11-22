using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntroState : AbstractPlayerState
{

    private bool isFalling = false;
    public override void EnterState(PlayerStateMachine context)
    {
        context.RB.gravityScale = 0;
        context.RB.velocity = (Vector2.up * context.introSpeed);
    }
    public override void UpdateState(PlayerStateMachine context)
    {

    }

    public override void FixedUpdateState(PlayerStateMachine context)
    {

    }

    public override void OnTriggerEnter2D(PlayerStateMachine context, Collider2D collision)
    {
        if (collision.CompareTag("Intro") && !isFalling){
            context.RB.velocity = Vector2.zero;
            context.RB.gravityScale = context.gravityScale;
            context.RB.AddForce(Vector2.up * context.introSpeed * context.introFall);
            isFalling = true;
        }

    }

    public override void OnCollisionEnter2D(PlayerStateMachine context, Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isFalling)
        {
            context.SwitchState(context.IdleState);
        }
    }

}
