using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Ayarlar")]
    public float speed = 3f;
    public float targetY = 50f;
    public bool stopAtTarget = true;

    private bool isMoving = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Asansörün yer çekiminden etkilenmemesi için
        if (rb) rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
             // Hedefe ulaşıldı mı?
            if (stopAtTarget && transform.position.y >= targetY)
            {
                rb.linearVelocity = Vector2.zero;
                isMoving = false;
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            }
            else
            {
                rb.linearVelocity = Vector2.up * speed;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Oyuncu asansöre bindi, hareketi başlat
            isMoving = true;
            
            // Kullanıcı isteği üzerine 'yapışma' (parenting) kodu kaldırıldı.
        }
    }
}
