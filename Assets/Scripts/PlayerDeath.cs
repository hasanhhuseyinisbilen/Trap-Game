using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (
            collision.gameObject.CompareTag("Trap") ||
            collision.gameObject.CompareTag("Icicle")
           )
        {
            RestartLevel();
        }
    }

    void RestartLevel()
    {
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
    }
}
