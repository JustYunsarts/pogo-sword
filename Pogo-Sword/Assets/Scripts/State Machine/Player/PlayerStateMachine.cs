using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStateMachine : MonoBehaviour
{
    //game screen variables
    [SerializeField]
    private Animator _transition;
    public Animator transition { get { return _transition; } private set { _transition = value; } }

    //general player variables
    [SerializeField]
    private GameObject gator;
    public GameObject Gator { get { return gator; } private set { gator = value; } }

    [SerializeField]
    private Animator _animator;
    public Animator animator { get { return _animator; } private set { _animator = value; } }

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Rigidbody2D _rb;
    public Rigidbody2D RB { get { return _rb; } private set { _rb = value; } }
    private Vector3 respawnPoint;

    [SerializeField]
    private float _gravityScale;
    public float gravityScale { get { return _gravityScale; } private set { _gravityScale = value; } }

    [SerializeField]
    private float _maxVelocity;
    public float maxVelocity { get { return _maxVelocity; } private set { _maxVelocity = value; } }


    //intro state variables
    [SerializeField]
    private float _introSpeed;
    public float introSpeed { get { return _introSpeed; } private set { _introSpeed = value; } }

    [SerializeField]
    private float _introFall;
    public float introFall { get { return _introFall; } private set { _introFall = value; } }

    //idle variables
    [SerializeField]
    private LayerMask _groundChecker;
    public LayerMask groundChecker { get { return _groundChecker; } private set { _groundChecker = value; } }

    [SerializeField]
    private float _feetRadius;
    public float feetRadius { get { return _feetRadius; } private set { _feetRadius = value; } }

    [SerializeField]
    private Transform _rightFoot; //used for checking if we need to switch to the fallstate from idle
    //if you walk off a platform, feetpos will not be in contact w/ the ground so we switch states
    public Transform rightFoot { get { return _rightFoot; } private set { _rightFoot = value; } }

    [SerializeField]
    private Transform _leftFoot; //used for checking if we need to switch to the fallstate from idle
    //if you walk off a platform, feetpos will not be in contact w/ the ground so we switch states
    public Transform leftFoot { get { return _leftFoot; } private set { _leftFoot = value; } }

    //movement variables 
    private Vector2 _horizontalMovement;
    public float horizontalMovment { get { return _horizontalMovement.x; } private set { _horizontalMovement.x = value; } }
    
    [SerializeField]
    private float _walkSpeed;
    public float walkSpeed { get { return _walkSpeed; } private set { _walkSpeed = value; } }


    //jumping variables
    private bool _jumpDown;
    public bool jumpDown { get { return _jumpDown; } private set { _jumpDown = value; } }
    
    private bool _jumpUp;
    public bool jumpUp { get { return _jumpUp; } private set { _jumpUp = value; } }
    
    [SerializeField]
    private float _jumpForce;
    public float jumpForce { get { return _jumpForce; } private set { _jumpForce = value; } }
    
    [SerializeField]
    private float _jumpGravity;
    public float jumpGravity { get { return _jumpGravity; } private set { _jumpGravity = value; } }
    [SerializeField]
    private float _coyoteTime;
    public float coyoteTime { get { return _coyoteTime; } private set { _coyoteTime = value; }  }
    
    //pogo variables
    //>_airDI is how much influence the player has in horizontal movement in the air
    [SerializeField]
    private float _airDI;
    public float airDI { get { return _airDI; } private set { _airDI = value; } }
    [SerializeField]
    private float _pogoGravity;
    public float pogoGravity { get { return _pogoGravity; } private set { _pogoGravity = value; } }
    [SerializeField]
    private float _pogoThreshold;
    public float pogoThreshold { get { return _pogoThreshold; } private set { _pogoThreshold = value; } }
    [SerializeField]
    private float _pogoMin;//minimun bounce strength
    public float pogoMin { get { return _pogoMin; } private set { _pogoMin = value; } }
    [SerializeField]
    private float _pogoStrength;
    public float pogoStrength { get { return _pogoStrength; } private set { _pogoStrength = value; } }
    [SerializeField]
    private float _pogoDecay;
    public float pogoDecay { get { return _pogoDecay; } private set { _pogoDecay = value; } }
    [SerializeField]
    private float _wallBounce;
    public float wallBounce { get { return _wallBounce; } private set { _wallBounce = value; } }



    // This is the context file that every player state will refer to
    AbstractPlayerState currentState;
    public AbstractPlayerState CurrentState { get { return currentState; } private set { currentState = value; } }

    public PlayerIntroState IntroState = new PlayerIntroState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerPogoState PogoState = new PlayerPogoState();
    public PlayerDeathState DeathState = new PlayerDeathState();
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerFallState FallState = new PlayerFallState();
    public PlayerMoveState MoveState = new PlayerMoveState();


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetFloat("respawnX") != 0) //logic for when respawning
        {
            respawnPoint = new Vector2(PlayerPrefs.GetFloat("respawnX"), PlayerPrefs.GetFloat("respawnY"));
            RB.transform.position = respawnPoint;
            currentState = IdleState;
            currentState.EnterState(this);
        }
        else
        {
            // the initial state of the player character
            currentState = IntroState;
            currentState.EnterState(this);
        }
    }

    //This script is a monobehavior, so this is for giving each state script (which derive from AbstractPlayerState) 
    //access to these monobehavior scripts.
    void OnTriggerEnter2D(Collider2D collision)
    {
        //because checkpoint setting applies to basically every state, I added the logic here
        if (collision.CompareTag("Respawn"))
        {
            respawnPoint = collision.transform.position;
            //save system for checkpoints
            PlayerPrefs.SetFloat("respawnX", respawnPoint.x);
            PlayerPrefs.SetFloat("respawnY", respawnPoint.y);
        }
        currentState.OnTriggerEnter2D(this, collision);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(currentState);
        }
        getMovement();
        currentState.UpdateState(this);
    }
    // Late update for each state (used for applying movement to the player rigid body)
    private void FixedUpdate()
    {
        RB.velocity = Vector2.ClampMagnitude(RB.velocity, maxVelocity);
        currentState.FixedUpdateState(this);
    }

    //we populate the horizontal movement (-1, 0, or 1) here so we can access it in multiple states
    //For example, we need it to switch from idle to movement states, as well as the movement state logic itself.
    //We might also need it to have directional inputs when the player is moving midair; we want them to have less
    //control, but still influence over their movement. 
    private void getMovement()
    {
        _horizontalMovement.x = Input.GetAxisRaw("Horizontal");
        _jumpDown = Input.GetButtonDown("Jump");
        _jumpUp = Input.GetButtonUp("Jump");
    }

    //used to flip the player sprite when moving back and forth
    public void flip()
    {
        if (_horizontalMovement.x == -1 && player.rotation.y == 0)
        {
            player.eulerAngles = new Vector3(0, 180, 0);
        }
        if (_horizontalMovement.x == 1 && player.rotation.y == -1)
        {
            player.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void SwitchState(AbstractPlayerState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    void PlayerRespawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("respawnX", 0);
        PlayerPrefs.SetFloat("respawnY", 0);
    }

    public bool IsGrounded()
    {
        //true if either the right or left foot is nearby a ground plane
        if (Physics2D.OverlapCircle(rightFoot.position, feetRadius, groundChecker) || Physics2D.OverlapCircle(leftFoot.position, feetRadius, groundChecker))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

}
