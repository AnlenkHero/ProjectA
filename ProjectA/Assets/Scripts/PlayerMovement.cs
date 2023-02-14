using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private float smoothInputSpeed;
    
    public Animator animator;
    private Vector3 _playerVelocity;
    private bool _isGrounded=true;
    private bool _ableToMakeADoubleJump = true;

    private Vector2 _smoothInputVelocity;
    private Vector2 _currentInputVector;
    private InputAction _jumpAction;
    private InputAction _moveAction;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

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
        //Makes input not straight(0/1) it slows down and speeds up linearly if its not necessary use input.x instead of _currentInputVector.x in playerVelocity.x
        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, input, ref _smoothInputVelocity, smoothInputSpeed);
        _playerVelocity.x = _currentInputVector.x*playerSpeed;

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
