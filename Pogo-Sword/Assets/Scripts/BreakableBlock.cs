using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject block;
    [SerializeField]
    private float _breakBounce;
    [SerializeField]
    private float _recoverTime;

    private bool isBroken = false;
    private bool shouldBreak = false;

    private SpriteRenderer blockSprite;
    private BoxCollider2D blockCollider;
    private float breakTime;
    private PlayerStateMachine machine;
    private GameObject player;
    private Rigidbody2D RB;

    private void Start()
    {
        blockSprite = block.GetComponent<SpriteRenderer>();
        blockCollider = block.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        machine = player.GetComponent<PlayerStateMachine>();
        RB = player.GetComponent<Rigidbody2D>();
        
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
        if (collision.gameObject.CompareTag("Player") && machine.CurrentState == machine.PogoState && shouldBreak)
        {
            RB.velocity = new Vector2(RB.velocity.x, _breakBounce);
            isBroken = true;
            breakTime = Time.time;
            shouldBreak = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //making sure that the block only breaks when you collide with the top plane
        shouldBreak = true;
    }
}
