using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private bool _isMovingRight = true;
    public GameObject bulletPrefab; // Tham chieu vien dan
    public Transform guntransform; // Tham chieu vi tri sung
    private float cooldownTime = 5f; // Thời gian hồi chiêu là 5 giây
    private float nextFireTime = 0f; // Thời gian tiếp theo có thể bắn
    void Start()
    {
        
    }

   
    void Update()
    {
        FreeFire();
    }
    private void FreeFire()
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
