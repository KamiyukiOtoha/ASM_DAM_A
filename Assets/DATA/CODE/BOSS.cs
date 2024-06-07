using System.Collections;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    private int healthBoss = 10000;
    [SerializeField] private GameObject bulletPrefabBoss; // Prefab cho viên đạn
    [SerializeField] private Transform bulletSpawnPointBoss; // Điểm xuất hiện của viên đạn

    private bool isAttacking = false;
    private Animator animatorBoss;

    void Start()
    {
        animatorBoss = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAttacking)
        {
            // Nếu đang tấn công, thực hiện hành động tấn công (nếu cần)
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Khi nhân vật vẫn còn chạm vào vùng collider của boss
            isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Khi nhân vật rời khỏi vùng collider của boss
            isAttacking = false;
        }
    }

    private void Attack()
    {
        animatorBoss.SetBool("ATK", true);
        ShootBoss();
    }

    private void ShootBoss()
    {
        var bullet = Instantiate(bulletPrefabBoss, bulletSpawnPointBoss.position, Quaternion.identity);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Tính hướng của đạn dựa trên vị trí của nhân vật
        Vector2 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - bulletSpawnPointBoss.position).normalized;
        bulletRb.velocity = direction * 10; // Tốc độ đạn

        // Tắt animation tấn công sau khi bắn xong
        animatorBoss.SetBool("ATK", false);
    }
}
