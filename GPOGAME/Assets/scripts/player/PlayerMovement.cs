
using System;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerAttack;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    


    private Rigidbody _rb;
    private Vector3 _movementVector;
    private Vector3 _jumptVector;
    public float Speed;
    private bool _isGrounded;
    public float JumpForce;
    private Animator _animator;
    private bool _isAttacking;
    private PlayerAttack _playerAttackmovemvent;
    

    void Start()
    {
        _rb= GetComponent<Rigidbody>(); ;
        _animator=GetComponent<Animator>();
        _playerAttackmovemvent= GetComponent<PlayerAttack>();
        _playerAttackmovemvent.IsAttacking+=AttackBegin;
        _playerAttackmovemvent.IsNotAttacking+=AttackStop;
}


    private void Update()
    {

        if (Input.GetKey(KeyCode.Space) && _isGrounded==true)
        {
            
            _rb.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode.Impulse);
            
        }

    }
    void FixedUpdate()
    {
        if (!_isAttacking)
        {

            if (Input.GetKey("w") | Input.GetKey("s") | Input.GetKey("a") | Input.GetKey("d"))
            {
                _animator.SetBool("Walk", true);
            }
            else
            {
                _animator.SetBool("Walk", false);
            }
            if (_isGrounded == true)
            {

                _movementVector = transform.right * Input.GetAxis("Horizontal")
                    + Input.GetAxis("Vertical") * transform.forward;
                _rb.MovePosition(transform.position + _movementVector * Speed * Time.fixedDeltaTime);
            }
            else
            {

                _movementVector = transform.right * Input.GetAxis("Horizontal")
                    + Input.GetAxis("Vertical") * transform.forward;
                _rb.MovePosition(transform.position + _movementVector * (float)0.5 * Speed * Time.fixedDeltaTime);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        IsGroundedCheck(collision, true);
    }
    private void OnCollisionExit(Collision collision) {
        IsGroundedCheck(collision, false);
    }

    private void IsGroundedCheck(Collision collision, bool value) {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
    private void AttackBegin()
    {
        _animator.SetBool("Walk", false);
        _isAttacking = true;
    }
    private void AttackStop()
    {
        _isAttacking = false;
    }


    
}
