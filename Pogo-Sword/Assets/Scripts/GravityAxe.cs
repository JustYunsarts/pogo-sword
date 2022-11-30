using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAxe : MonoBehaviour
{
    /*Gravity Axe is a prefab axe projectile shot out by an axe spawner.
     * The spawner's expected behavior is that it shoots out a projectile every set interval
     * The projectile is not affected by the unity's physics engine, moving in a straight line at a constant speed
     * It is also always spinning to give the impression of motion */


    [SerializeField]
    private float spinSpeed;

    [SerializeField]
    private float moveSpeed;

    private GravityAxeSpawner parent;
    private Transform _transform;
    private Rigidbody2D rb;

    private PlayerStateMachine machine;
    private GameObject player;
    private Vector2 axeDirection;



    // Start is called before the first frame update
    void Start()
    {
        _transform = this.gameObject.GetComponent<Transform>();
        parent = this.gameObject.transform.parent.gameObject.GetComponent<GravityAxeSpawner>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        machine = player.GetComponent<PlayerStateMachine>();

    }


    private void FixedUpdate()
    {
        _transform.Rotate(new Vector3(0,0,spinSpeed));

        //access direction variable in spawner script and apply velocity
        if (parent.Direction == GravityAxeSpawner.directions.up)
        {
            axeDirection = Vector2.up;
        }
        if (parent.Direction == GravityAxeSpawner.directions.down)
        {
            axeDirection = Vector2.down;
        }
        if (parent.Direction == GravityAxeSpawner.directions.left)
        {
            axeDirection = Vector2.left;
        }
        if (parent.Direction == GravityAxeSpawner.directions.right)
        {
            axeDirection = Vector2.right;
        }


        rb.velocity = axeDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //move to death state
        {
            machine.SwitchState(machine.DeathState);
        }
        if (collision.CompareTag("Despawn"))
        {
            Destroy(this.gameObject);
        }
    }
}
