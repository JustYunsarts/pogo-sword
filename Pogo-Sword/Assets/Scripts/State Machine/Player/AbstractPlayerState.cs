
using UnityEngine;

public abstract class AbstractPlayerState
{
    public abstract void EnterState(PlayerStateMachine context);
    public abstract void UpdateState(PlayerStateMachine context);
    public abstract void FixedUpdateState(PlayerStateMachine context);
    public abstract void OnTriggerEnter2D(PlayerStateMachine context, Collider2D collision);
    public abstract void OnCollisionEnter2D(PlayerStateMachine context, Collision2D collision);

}
