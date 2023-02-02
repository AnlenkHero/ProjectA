using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [Tooltip("GameObject attached at the lowest Y position to this GameObject")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private PlayerInput playerInput;
    
    public Animator animator;
    private Vector3 _playerVelocity;
    private bool _isGrounded=true;
    private bool _ableToMakeADoubleJump = true;
    

    private InputAction _jumpAction;
    private InputAction _moveAction;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Grounded = Animator.StringToHash("grounded");

    private void Awake()
    {
        _jumpAction = playerInput.actions["Jump"];
        _moveAction = playerInput.actions["Move"];
    }
    void Update()
    {
        _isGrounded = controller.isGrounded;
        animator.SetBool(Grounded,_isGrounded);
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -1f;
        }

        Vector2 input = _moveAction.ReadValue<Vector2>();
        _playerVelocity.x = input.x*playerSpeed;

        if (_jumpAction.triggered && _isGrounded)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(_playerVelocity * Time.deltaTime);
        
        animator.SetFloat(Speed,MathF.Abs(input.x));

        FlipRotationY(input);
    }

    private void FlipRotationY(Vector2 move)
    {
        if (move != Vector2.zero)
        {
            gameObject.transform.forward = new Vector3(move.x,0,0);
        }
    }
}
