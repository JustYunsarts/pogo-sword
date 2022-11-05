using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PogoBehavior : MonoBehaviour
{
    [SerializeField]
    private PhysicsMaterial2D gatorPhys;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float bounceStrength;
    [SerializeField]
    private float minBounce;
    [SerializeField]
    private float bounceGravity;
    [SerializeField]
    private float bounceDecay;

    private float tempGravity;
    private float Vely;
    private bool shiftDown;
    private bool shiftUp;

    // Update is called once per frame
    void Update()
    {
        Getinputs();
        // checking player state manager, if the player is in a jumping state, then we allow pogo stance if shift is pressed 
        if (PlayerStateManager.state == PlayerStateManager.CurrentState.Jumping && shiftDown)
        {
            Pogo();
        }
        // Undo the stance if you let go
        if (animator.GetBool("isPogo") == true && shiftUp)
        {
            PogoRevert();
        }

    }

    private void Getinputs()
    {
        shiftDown = Input.GetKeyDown(KeyCode.LeftShift);
        shiftUp = Input.GetKeyUp(KeyCode.LeftShift);
    }

    private void Pogo()
    {
        animator.SetBool("isPogo", true);
        //very briefly increase gravity so that you get a burst of speed when you enter pogo stance
        tempGravity = rb.gravityScale;
        rb.gravityScale = bounceGravity;
    }

    private void PogoRevert()
    {
        animator.SetBool("isPogo", false);
        //reset the gravity back here
        rb.gravityScale = tempGravity;
    }

    private void FixedUpdate()
    {
        //keep track of your vertical velocity when in pogo stance for the bounce logic
        if(animator.GetBool("isPogo") == true)
        {
            Vely = rb.velocity.y;
        }
    }

    //logic for bouncing on the ground.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" && animator.GetBool("isPogo"))
        {
            if(Vely < -minBounce)
            {
                rb.velocity = new Vector2(rb.velocity.x, (-Vely * bounceDecay) * bounceStrength);

                Vely = 0;
            }
        }
    }
}
