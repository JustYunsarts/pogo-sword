using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject block;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float _breakBounce;
    [SerializeField]
    private float _recoverTime;
    [SerializeField]
    private Rigidbody2D RB;

    private bool isBroken = false;
    private SpriteRenderer blockSprite;
    private BoxCollider2D blockCollider;
    private float breakTime;
    private PlayerStateMachine machine;

    private void Start()
    {
        blockSprite = block.GetComponent<SpriteRenderer>();
        blockCollider = block.GetComponent<BoxCollider2D>();
        machine = player.GetComponent<PlayerStateMachine>();

    }

    private void FixedUpdate()
    {
        //To make the breaking of the block happen, we disable and enable the block's sprite renderer + collider
        if (isBroken)
        {
            blockSprite.enabled = false;
            blockCollider.enabled = false;
        }

        if(Time.time >= breakTime + _recoverTime)
        {
            blockSprite.enabled = true;
            blockCollider.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && machine.CurrentState == machine.PogoState)
        {
            RB.velocity = new Vector2(RB.velocity.x, _breakBounce);
            isBroken = true;
            breakTime = Time.time;
        }

    }
}
