using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPogoState : AbstractPlayerState
{
    bool repeatBounce;
    bool isBounce;
    float Vely;
    float bounceStrength;

    bool wallBouncing;

    public override void EnterState(PlayerStateMachine context)
    {
        context.animator.SetBool("isPogo", true);
        context.RB.gravityScale = context.pogoGravity;
        repeatBounce = false;
        wallBouncing = false;
        isBounce = false;
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
        if (!wallBouncing)
        {
            //if bouncing has not happened (you are falling), we apply airDI to decrease horizontal movement
            if (isBounce == false)
            {
                context.RB.velocity =
                    Vector2.ClampMagnitude(new Vector2(context.horizontalMovment * context.walkSpeed * context.airDI, context.RB.velocity.y), context.maxVelocity);
            }
            //upon bounce, we don't apply airDI.
            if (isBounce)
            {
                context.RB.velocity =
                    (new Vector2(context.horizontalMovment * context.walkSpeed, context.RB.velocity.y));
            }
        }
    }
    public override void OnTriggerEnter2D(PlayerStateMachine context, Collider2D collision)
    {

        if (collision.CompareTag("Hazard"))
        {
            context.animator.SetBool("isPogo", false);
            context.SwitchState(context.DeathState);
        }
    }

    public override void OnCollisionEnter2D(PlayerStateMachine context, Collision2D collision)
    {
        //reset gravity here to normal so that bounciness isn't hampered
        context.RB.gravityScale = context.gravityScale;

        //make sure that the bounce will only happen when colliding with horizontal plane

        if (context.IsGrounded())
        {
            isBounce = true;
        }

        if (wallBouncing)
        {
            wallBouncing = false;
        }

        //We check if the collision is with the ground, as we might want to have different bounce logics with different
        //special interactable blocks; those would be handled in their own scripts (eg. the breakable block bounce will
        //be dealt with in the breakable blocks script).
        //We do this so that this one pogo state isn't handling every single different variations of a bounce, which will
        //render this state machine pointless
        if (collision.gameObject.CompareTag("Ground") && context.IsGrounded()) {
            //We add velocity here to ensure that the bounce velocity is only added once, upon collision
            //We add the full bounce amount when it's your first collision with the ground
            if (Vely < -context.pogoThreshold && repeatBounce == false)
            {
                bounceStrength = (-Vely * context.pogoDecay) * context.pogoStrength;
                context.RB.velocity = new Vector2(context.RB.velocity.x, bounceStrength);

                repeatBounce = true;
            }
            //Repeated bounces (holding shift) will otherwise give you diminishing results
            else if (Vely < -context.pogoThreshold && repeatBounce)
            {
                context.RB.velocity = 
                    new Vector2(context.RB.velocity.x, (-Vely * context.pogoDecay));

            }
        }
        else if (collision.gameObject.CompareTag("Ground") && context.IsGrounded() == false)
        {
            wallBouncing = true;
            context.RB.velocity =
                    new Vector2(-context.horizontalMovment * context.wallBounce, Vely);

        }
    }
}
