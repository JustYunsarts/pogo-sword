using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    private float walkVelocity;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float deceleration;
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
    private bool canJump;
    private bool jumpkey;
    private bool JumpUp;

    [SerializeField]
    private Transform feetpos;
    [SerializeField]
    private float feetRadius;
    public LayerMask groundChecker;
    [SerializeField]
    private float jumpholdtime;

    private float jumpTimeCounter;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        getMovement();
        calculateSpeed();
        calculateJump();
    }

    void getMovement()
    {
        ApplyMove.x = Input.GetAxisRaw("Horizontal");
        JumpDown = Input.GetButtonDown("Jump");
        JumpUp = Input.GetButtonUp("Jump");
        jumpkey = Input.GetButton("Jump");
    }

    void calculateSpeed()
    {
        //logic for whenever you are actually moving
        if (ApplyMove.x != 0)
        {
            horizontalSpeed += ApplyMove.x * acceleration * Time.deltaTime;
            horizontalSpeed = Mathf.Clamp(horizontalSpeed, -walkVelocity, walkVelocity);
        }

        //when you are not pressing anything, you wanna decelerate to 0
        else
        {
            horizontalSpeed = Mathf.MoveTowards(horizontalSpeed, 0, deceleration * Time.deltaTime);
        }
    }
    private void calculateJump()
    {
        //if the Transform feetpos is touching something with the "Ground" tag, we set isGround to true
        isGrounded = Physics2D.OverlapCircle(feetpos.position, feetRadius, groundChecker);

        //if JumpDown is true, then this is the frame that you pressed jump. if isgrounded is true, then you can really jump.
        if(JumpDown && isGrounded )
        {
            rb.velocity = Vector2.up * jumpForce;
            canJump = true;
            jumpTimeCounter = jumpholdtime;
        }

/*        //if you hold space, you should be able to jump higher. 
        if(jumpkey && canJump)
        {
            if(jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                canJump = false;
            }
        }*/

        //if you let go of jump at any point, you can't jump again
        if(JumpUp)
        {
            //canJump = false;
            rb.velocity = Vector2.up * -rb.velocity;
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = (new Vector2(horizontalSpeed,rb.velocity.y));
    }
}

