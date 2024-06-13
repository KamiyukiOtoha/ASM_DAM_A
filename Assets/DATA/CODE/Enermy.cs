using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enermy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;
    

    private int health;
    private float targetPositionX;
    private float moveRange = 2f; // Phạm vi di chuyển ngẫu nhiên mỗi lần
    private float maxRange = 4f;  // Phạm vi tối đa từ vị trí ban đầu
    private float initialPositionX;

    void Start()
    {
        health = 1000;
       
        initialPositionX = transform.position.x; // Lưu vị trí ban đầu
        SetNewTargetPosition();
    }

    void Update()
    {
        MoveTowardsTarget();
    }

    private void SetNewTargetPosition()
    {
        // Chọn một vị trí mới ngẫu nhiên trên trục x trong phạm vi 2 đơn vị từ vị trí hiện tại
        float newTargetPositionX = transform.position.x + Random.Range(-moveRange, moveRange);

        // Đảm bảo vị trí mới không vượt quá phạm vi tối đa từ vị trí ban đầu
        newTargetPositionX = Mathf.Clamp(newTargetPositionX, initialPositionX - maxRange, initialPositionX + maxRange);

        targetPositionX = newTargetPositionX;
    }

    private void MoveTowardsTarget()
    {
        // Di chuyển đến vị trí đích trên trục x
        Vector3 targetPosition = new Vector3(targetPositionX, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Kiểm tra nếu đã đến vị trí đích, thì chọn một vị trí đích mới
        if (Mathf.Abs(transform.position.x - targetPositionX) < 0.1f)
        {
            SetNewTargetPosition();
        }

        // Cập nhật scale hiện tại để hướng enemy di chuyển đúng hướng
        var currentScale = transform.localScale;
        if ((targetPositionX > transform.position.x && currentScale.x < 0) ||
            (targetPositionX < transform.position.x && currentScale.x > 0))
        {
            currentScale.x *= -1;
        }

        transform.localScale = currentScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullett"))
        {
            // Hủy viên đạn
            Destroy(other.gameObject);
            health -= 1000;
           
            if (health <= 0)
            {
                // Hủy enemy khi hết máu
                Destroy(gameObject);
            }
        }
    }
}
