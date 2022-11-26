using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : AbstractPlayerState
{
    //respawning is done via animation events, so that it can be easily timed to play after the death animation.
    //This state is used to trigger the death animation
    public override void EnterState(PlayerStateMachine context)
    {
        context.animator.SetTrigger("isDead");
    }
    public override void UpdateState(PlayerStateMachine context)
    {

    }

    public override void FixedUpdateState(PlayerStateMachine context)
    {

    }

    public override void OnTriggerEnter2D(PlayerStateMachine context, Collider2D collision)
    {
    }

    public override void OnCollisionEnter2D(PlayerStateMachine context, Collision2D collision)
    {

    }
}

