//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//public class BOSS : MonoBehaviour
//{
//    // Start is called before the first frame update
//    [SerializeField]
//    private float leftBoundary;
//    [SerializeField]
//    private float rightBoundary;
//    [SerializeField]
//    private bool _isMovingRight = true;
//    [SerializeField]
//    private float moveSpeed = 1f;

//    [SerializeField] private Slider _healthSlider;
//    private int health;
//    [SerializeField] private ParticleSystem _hitEffect;
//    void Start()
//    {
//        health = 10000;
//        _healthSlider.maxValue = health;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Lấy vị trí hiện tại
//        var currenPosition = transform.position;
//        if (currenPosition.x > rightBoundary)
//        {
//            // Niếu vị trí hiện tại của snail > rightBoundary
//            // Di chuyển sang trái
//            _isMovingRight = false;
//        }
//        else if (currenPosition.x < leftBoundary)
//        {
//            // Niếu vi trí hiện tại của Snail < leftBoundary
//            // Di chuyển sang phải
//            _isMovingRight = true;
//        }
//        // Di chuyển ngang 
//        // (1,0,0) * 1 * 0.02 = (0.02,0,0)
//        var direction = Vector3.right;
//        if (_isMovingRight == false)
//        {
//            direction = Vector3.left;
//        }
//        // var direction = _isMovingRight ? Vector3.right : Vector3.left; cái này bằng if trên
//        transform.Translate(direction * moveSpeed * Time.deltaTime);

//        // Scale Hiện tại
//        // Trái < 0 Phải >0
//        var currentSacle = transform.localScale;
//        if ((_isMovingRight && currentSacle.x < 0) || (_isMovingRight == false && currentSacle.x > 0))
//        {
//            currentSacle.x *= -1;
//        }


//        transform.localScale = currentSacle;
//    }
//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.gameObject.CompareTag("Bullet"))
//        {
//            // Hiu ung no
//            var hitEffect = Instantiate(_hitEffect, // tẠO RA CÁI GÌ 
//                transform.position, // VỊ TRÍ 
//                Quaternion.identity); // GÓC QUAY
//            Destroy(hitEffect.gameObject, 1f);
//            // Bien mat vien dan
//            Destroy(other.gameObject);
//            health -= 500;
//            _healthSlider.value = health;
//            if (health <= 0)
//            {
//                // het hp thi chet
//                Destroy(gameObject);
//            }
//        }
//    }
//}