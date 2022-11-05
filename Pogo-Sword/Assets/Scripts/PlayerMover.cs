using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gatorSprite;
    [SerializeField]
    private float walkVelocity;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Collider2D gator_Collider;

    private Vector2 ApplyMove;
    private float horizontalSpeed;

    private bool JumpDown;
    private bool isGrounded;
    private bool JumpUp;

    [SerializeField]
    private Transform feetpos;
    [SerializeField]
    private float feetRadius;
    public LayerMask groundChecker;
    [SerializeField]


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        getMovement();
        calculateJump();
        Flip();
    }

    void getMovement()
    {
        ApplyMove.x = Input.GetAxisRaw("Horizontal");
        JumpDown = Input.GetButtonDown("Jump");
        JumpUp = Input.GetButtonUp("Jump");
    }

    private void calculateJump()
    {
        //if the Transform feetpos is touching something with the "Ground" tag, we set isGround and canJump to true
        isGrounded = Physics2D.OverlapCircle(feetpos.position, feetRadius, groundChecker);

        //if JumpDown is true, then this is the frame that you pressed jump. if isgrounded is true, then you can really jump.
        if(JumpDown && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if(JumpUp && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = (new Vector2(ApplyMove.x * walkVelocity , rb.velocity.y));

    }

    private void Flip()
    {
        if(ApplyMove.x == -1)
        {
            gatorSprite.flipX = true;
        }
        if(ApplyMove.x == 1)
        {
            gatorSprite.flipX = false;
        }
    }
}
