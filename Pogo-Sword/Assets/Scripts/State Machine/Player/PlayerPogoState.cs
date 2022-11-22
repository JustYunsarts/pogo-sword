using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPogoState : AbstractPlayerState
{
    bool repeatBounce;
    float Vely;
    float bounceStrength;
    public override void EnterState(PlayerStateMachine context)
    {
        context.animator.SetBool("isPogo", true);
        context.RB.gravityScale = context.pogoGravity;
        repeatBounce = false;
    }
    public override void UpdateState(PlayerStateMachine context)
    {
        Vely = context.RB.velocity.y;
        context.flip();
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            context.animator.SetBool("isPogo", false);
            context.SwitchState(context.FallState);
        }
    }
    public override void FixedUpdateState(PlayerStateMachine context)
    {
        context.RB.velocity = 
            (new Vector2(context.horizontalMovment * context.walkSpeed * context.airDI, context.RB.velocity.y));

    }
    public override void OnTriggerEnter2D(PlayerStateMachine context, Collider2D collision)
    {
    }

    public override void OnCollisionEnter2D(PlayerStateMachine context, Collision2D collision)
    {
        //reset gravity here to normal so that bounciness isn't hampered
        context.RB.gravityScale = context.gravityScale;

        //bounciness logic
        if (Vely < -context.pogoThreshold && repeatBounce == false)
        {
            bounceStrength = Mathf.Clamp((-Vely * context.pogoDecay) * context.pogoStrength, 0f, context.maxVelocity);
            context.RB.velocity = new Vector2(context.RB.velocity.x,bounceStrength);

            Vely = 0;
            repeatBounce = true;
        }
        else if (Vely < -context.pogoThreshold && repeatBounce)
        {
            context.RB.velocity = new Vector2(context.RB.velocity.x, (-Vely * context.pogoDecay));

        }
    }
}
