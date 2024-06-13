using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BOSS : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefabBoss; // Prefab cho viên đạn
    [SerializeField] private Transform bulletSpawnPointBoss; // Điểm xuất hiện của viên đạn
    [SerializeField] private Slider healthSliderBoss; // Thanh máu của boss
    [SerializeField] private TextMeshProUGUI _healthTextboss; // Tham chiếu tới TextMeshProUGUI hiển thị mạng
    [SerializeField] private GameObject _CanavasGame;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;

    private bool isAttacking = false;
    private Animator animatorBoss;

    private int _maxhp = 10000;
    private static int _Hp;
    private float cooldownTime = 2f; // Thời gian chờ giữa các lần bắn
    private float nextFireTime = 0f; // Thời gian bắn đạn tiếp theo

    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        animatorBoss = GetComponent<Animator>();
        _Hp = _maxhp;
        healthSliderBoss.maxValue = _maxhp;
        healthSliderBoss.value = _maxhp;
        _healthTextboss.text = "Health: " + _Hp.ToString();
    }

    void Update()
    {
        if (isAttacking && Time.time > nextFireTime)
        {
            // Nếu đang tấn công và đã đến thời gian bắn mới
            nextFireTime = Time.time + cooldownTime; // Cập nhật thời gian bắn tiếp theo
            ShootBoss(); // Thực hiện bắn
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullett"))
        {
            // Nếu va chạm với đạn
            TakeDamage(1000); // Giảm máu của boss
            Destroy(other.gameObject); // Hủy đối tượng đạn
            // Boss sẽ tấn công ngay lập tức khi bị trúng đạn
            ShootBoss();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            // Khi nhân vật vào phạm vi tấn công của boss
            isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Khi nhân vật ra khỏi phạm vi tấn công của boss
            isAttacking = false;
        }
    }

    private void ShootBoss()
    {
        // Kích hoạt animation tấn công của boss
        animatorBoss.SetBool("ATK", true);

        var bullet = Instantiate(bulletPrefabBoss, bulletSpawnPointBoss.position, Quaternion.identity);
        var bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Tính hướng của đạn dựa trên vị trí của nhân vật
        Vector2 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - bulletSpawnPointBoss.position).normalized;
        bulletRb.velocity = direction * 0.9f; // Giảm tốc độ của đạn

        // Hủy đạn sau 5 giây
        Destroy(bullet, 5f);

        // Tắt animation tấn công sau khi bắn xong
        StartCoroutine(StopAttackAnimation());
    }

    private IEnumerator StopAttackAnimation()
    {
        // Đợi một khoảng thời gian ngắn trước khi tắt animation tấn công
        yield return new WaitForSeconds(0.2f);
        animatorBoss.SetBool("ATK", false);
    }

    public void TakeDamage(int damage)
    {
        _Hp -= damage;
        healthSliderBoss.value = _Hp;
        _healthTextboss.text = "Health: " + _Hp.ToString();

        if (_Hp <= 0)
        {
            // Xử lý khi boss bị hết máu
            Destroy(gameObject);
            _CanavasGame.SetActive(true);
        }
    }
}
