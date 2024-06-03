using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _jumpForce = 5f;

    Rigidbody2D _myRigidbody;
    private bool _isMovingRight = true;

    CapsuleCollider2D _myFeetCollider;
    private Animator _animator;
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myFeetCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        move();
        jump();
        ATK();
    }
    private void move()
    {

        var horizontalInput = Input.GetAxis("Horizontal");
        transform.localPosition += new Vector3(horizontalInput, 0, 0) * _moveSpeed * Time.deltaTime;
        if (horizontalInput > 0)
        {
            _isMovingRight = true;

            _animator.SetBool("Irun", true);


        }
        else if (horizontalInput < 0)
        {

            _isMovingRight = false;

            _animator.SetBool("Irun", true);

        }
        else
        {

            _animator.SetBool("Irun", false);

        }

        transform.localScale = _isMovingRight ? new Vector2(1f, 1f) : new Vector2(-1f, 1f);

        
    }
    private void jump()
    {
        if (_myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platform")) == false)
        {
            _animator.SetBool("Irun", false);
            return;
        }
        var veticalInput = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
        if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    _animator.SetBool("Jump", true);
        //    _animator.SetBool("Irun", false);
        //}
        //else
        //{
        //    _animator.SetBool("Jump", false);

        //}

        //if (veticalInput > 0)
        {
            _myRigidbody.velocity = new Vector2(0, _jumpForce);

        }
    }
    private void ATK()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _animator.SetBool("ATK", true);
        }
        else
        {
            _animator.SetBool("ATK", false);
        }
    }
}
