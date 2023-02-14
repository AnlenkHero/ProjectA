using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    public PlayerBaseState CurrentState;
    public CharacterController controller;
    
    
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float smoothInputSpeed;
    [SerializeField]
    private float rotateSpeed=3;
    public float jumpHeight = 2.0f;
    public float gravityValue = -9.81f;
    public bool isGrounded; // unnecessary, just to check in inspector for debug
    //private bool _ableToMakeADoubleJump = true;

    public Vector3 moveVector;
    public Vector3 playerVelocity;
    private Vector2 _smoothInputVelocity;
    private Vector2 _currentInputVector;
    
    public Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

    #region States

    public IdleState IdlingState = new IdleState();

    public RunState RunningState = new RunState();

    public FallState FallingState = new FallState();

    public JumpState JumpingState = new JumpState();

    #endregion
    
    void Start()
    {
        CurrentState = IdlingState;
        CurrentState.EnterState(this);
    }
    
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (CurrentState != JumpingState
            && CurrentState != FallingState 
            && !controller.isGrounded)
        {
            SwitchState(FallingState);
        }
        ApplyGravity();
        CurrentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        CurrentState.ExitState(this);
        CurrentState = state;
        state.EnterState(this);
    }

    #region MoveActions
    private void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector2>();
        //Makes input not straight(0/1) it slows down and speeds up linearly if its not necessary use input.x instead of _currentInputVector.x in playerVelocity.x
        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, moveVector, ref _smoothInputVelocity, smoothInputSpeed);
        playerVelocity.x = _currentInputVector.x*playerSpeed;
    }

    private void OnJump()
    {
        if (CurrentState != JumpingState && CurrentState != FallingState)
        {
            SwitchState(JumpingState);
        }
    }
    

    #endregion
    
    #region Movement
    public void ApplyGravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1f;
        }
    }

    public void Move()
    {
        controller.Move(playerVelocity * Time.deltaTime);
        RotateTowardsVector();
    }

    public void RotateTowardsVector()
    {
        Vector3 xDirection = new Vector3(moveVector.x, 0, 0);
        if (xDirection.magnitude == 0) return;
        Quaternion rotation = Quaternion.LookRotation(xDirection);
        transform.rotation=Quaternion.RotateTowards(transform.rotation,rotation,rotateSpeed);
    }
    #endregion
}
