using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [Header("DeadZone Ayarları")]
    [SerializeField] private float deathY = -10f; // Bu koordinatın altına düşerse ölür

    private bool isDead = false; // Ölüm koruması

    void Update()
    {
        // Eğer oyuncu belirlenen Y koordinatının altına düşerse
        if (!isDead && transform.position.y < deathY)
        {
            RestartLevel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return; // Zaten ölüyse işlem yapma

        if (
            collision.gameObject.CompareTag("Trap") ||
            collision.gameObject.CompareTag("Icicle") ||
            collision.gameObject.CompareTag("FlyBox") ||
            collision.gameObject.CompareTag("Diken")
           )
        {
            RestartLevel();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return; // Zaten ölüyse işlem yapma

        if (
            other.CompareTag("Trap") ||
            other.CompareTag("Icicle") ||
            other.CompareTag("FlyBox")||
            other.CompareTag("Diken")
           )
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
        if (isDead) return; // Zaten ölü ise tekrar çalıştırma

        isDead = true; // Ölüm flag'ini set et

        // Her ölümde can azalt
        if (LivesManager.Instance != null)
        {
            LivesManager.Instance.LoseLife();
        }

        SnowyLevelManager manager = FindObjectOfType<SnowyLevelManager>();
        if (manager != null)
        {
            manager.RestartLevel();
        }

        transform.position = Vector3.zero;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // 0.5 saniye sonra isDead flag'ini resetle
        Invoke("ResetDeathFlag", 0.5f);
    }

    void ResetDeathFlag()
    {
        isDead = false;
    }
}
