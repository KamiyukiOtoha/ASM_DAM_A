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

    public GameObject bulletPrefab; // Tham chieu vien dan
    public Transform guntransform; // Tham chieu vi tri sung
    private float cooldownTime = 5f; // Thời gian hồi chiêu là 5 giây
    private float nextFireTime = 0f; // Thời gian tiếp theo có thể bắn
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
        Fire();
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
    private void Fire()
    {
        // nhan phim f ban dan
        if (Input.GetKeyDown(KeyCode.E) && Time.time > nextFireTime)
        {
            // cập nhật thời gian hồi chiêu
            nextFireTime = Time.time + cooldownTime;

            // tao ra vien dan tai vi tri sung
            var onBullet = Instantiate(bulletPrefab, guntransform.position, Quaternion.identity);

            // Cho vien dan bay theo huong nhan vat
            var velocity = new Vector2(15f, 0);
            if (_isMovingRight == false)
            {
                velocity = new Vector2(-15f, 0);
            }

            onBullet.GetComponent<Rigidbody2D>().velocity = velocity;
            // Destroy Dan
            Destroy(onBullet, 2f);
        }
    }
}
