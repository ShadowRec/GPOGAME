
using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    


    private Rigidbody _rb;
    private Vector3 _movementVector;
    private Vector3 _jumptVector;
    public float Speed;
    private bool _isGrounded;
    public float JumpForce;
    public delegate void MoveEvent();
    public  MoveEvent IsMoving;
    private Animator _animator;
    void Start()
    {
        _rb= GetComponent<Rigidbody>(); ;
        _animator=GetComponent<Animator>();
    }


    private void Update()
    {

        if (Input.GetKey(KeyCode.Space) && _isGrounded==true)
        {
            
            IsMoving?.Invoke();
            _rb.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode.Impulse);
            
        }

    }
    void FixedUpdate()
    {
      
        
        if ((Input.GetAxis("Horizontal")!=0) ^ (Input.GetAxis("Vertical")!=0))
        {
            IsMoving?.Invoke();
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.SetBool("Walk", false);
        }
        if (_isGrounded==true)
        {
            
            _movementVector = transform.right * Input.GetAxis("Horizontal") 
                + Input.GetAxis("Vertical") * transform.forward;
            _rb.MovePosition(transform.position + _movementVector * Speed * Time.fixedDeltaTime);
        }
        else
        {
            
            _movementVector = transform.right * Input.GetAxis("Horizontal") 
                + Input.GetAxis("Vertical") * transform.forward;
            _rb.MovePosition(transform.position + _movementVector * (float)0.5* Speed * Time.fixedDeltaTime);
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

    


    
}
