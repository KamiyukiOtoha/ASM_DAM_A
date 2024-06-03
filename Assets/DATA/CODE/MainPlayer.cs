﻿using System.Collections;
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


    // Quản lý số mạng nhân vật
    [SerializeField]
    private int _maxhp = 100;
    private static int _Hp;
    // Biến static là biến tĩnh(biến của class) 
    // Tham chiếu đên Panel
    [SerializeField]
    private TextMeshProUGUI _healthText; // Tham chiếu tới TextMeshProUGUI hiển thị mạng

    //thanh slider
    [SerializeField]
    private Slider _healthSlider;
    // Phát nhạc 
    // Tham chiếu đên audio sorce
    // Tham chiếu đên Audioclip

    private AudioSource _audioSource; //  TRình phát âm thanh 
    [SerializeField]
    private AudioClip _CoinSound; // Trình pháp nhạc

    // Tham chiếu tới text dieem so
    [SerializeField]
    private TextMeshProUGUI _scoretext;
    private static int score = 0; // static de qua man 2 ko mat
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myFeetCollider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();

        _Hp = 100;
        _healthSlider.maxValue = _Hp;
        _healthText.text = "Health: " + _Hp.ToString();
        // Gan gia tri cho diem so
        _scoretext.text = score.ToString();

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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // Phát ra tiếng nhạc
            //_audioSource.PlayOneShot(_CoinSound);
            // Tăng Điểm
            score += 10;
            _scoretext.text = score.ToString();
            if (_Hp < _maxhp)
            {
                // Cập nhật số mạng khi chạm vào đồng xu
                _Hp += 10;
                // Cập nhật hiển thị số mạng
                _healthText.text = "Health: " + _Hp.ToString();
                _healthSlider.value = _Hp;
            }


            // Làm biết mất xu 
            Destroy(other.gameObject);
        }
    }
}