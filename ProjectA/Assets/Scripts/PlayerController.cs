using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [Tooltip("GameObject attached at the lowest Y position to this GameObject")]
    /*[SerializeField]
    private Transform groundCheck;
    [Tooltip("Layer which will be checked")]
    [SerializeField]
    private LayerMask groundMask;
    private float _groundDistance=0.3f;*/
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    public Animator animator;
    private Vector3 _playerVelocity;
    private bool _isGrounded=true;
    private bool _ableToMakeADoubleJump = true;

    void Update()
    {
        /* _isGrounded = Physics.CheckSphere(groundCheck.position, _groundDistance, groundMask);
if (_isGrounded && _playerVelocity.y < 0)
{
_playerVelocity.y = -2f;
} 
*/
         
        _isGrounded = controller.isGrounded;
        if (_isGrounded && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -1f;
            _ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            animator.SetFloat("jumpSpeed",_playerVelocity.y);
        }
        else
        {
            _playerVelocity.y += gravityValue * Time.deltaTime;
            if (_ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                DoubleJump();
            }
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        float speedForAnimator = move.x;
        animator.SetFloat("Speed",Mathf.Abs(speedForAnimator));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(_playerVelocity * Time.deltaTime);
        
        
    }
    
    private void DoubleJump()
    {
        _playerVelocity.y += Mathf.Sqrt((jumpHeight/2) * -3.0f * gravityValue);
        _ableToMakeADoubleJump = false;
    }
    private void Jump()
    {
        _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}