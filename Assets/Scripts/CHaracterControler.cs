using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using Unity.VisualScripting;


public class CHaracterControler : MonoBehaviour
{
    public Rigidbody2D _rigidBody;
    public Animator _animator;
    public InputAction moveAction;
    public Vector2 _moveInput;
    public InputAction jumpAction;
    public float _playerSpeed = 5;
    public float _jumpForce = 0.1f;
    public GroundSensor groundSensor;
    public Transform _sensorPosition;
    public Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    public bool _alreaduLanded = true;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions["Move"];
        jumpAction = InputSystem.actions["Jump"];
        groundSensor = GetComponentInChildren<GroundSensor>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        _moveInput = moveAction.ReadValue<Vector2>();
        Debug.Log(_moveInput);


        if (jumpAction.WasPressedThisFrame() && isGrounded())
        {
            Jump();
        }


        Movement();


        _animator.SetBool("IsJumping", !isGrounded());


    }


    void Jump()
    {
        _rigidBody.AddForce(transform.up * Mathf.Sqrt(_jumpForce * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);
    }


    void Movement()
    {
        if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRuning", true);
        }
        else if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRuning", true);
        }
        else
        {
            _animator.SetBool("IsRuning", false);
        }
    }


    void FixedUpdate()
    {
        _rigidBody.linearVelocity = new Vector2(_moveInput.x * _playerSpeed, _rigidBody.linearVelocityY);
    }


    bool isGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        foreach (Collider2D item in ground)
        {
            if (item.gameObject.layer == 3)
            {
                return true;
            }
        }
        return false;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);
    }


}

