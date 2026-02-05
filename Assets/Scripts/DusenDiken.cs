using UnityEngine;

public class DusenDiken : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool activates = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Should not be affected by gravity or move at first
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = true; // Should be seen by physics engine but not move
        }
    }

    public void StartFalling()
    {
        if (activates) return;
        activates = true;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 3f; // Can be adjusted for faster fall
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If it hits the player, the PlayerDeath script will handle it (if tag is spike).
        // In any case, destroy it when it hits something.
        
        // You can add Instantiate(breakEffect) here if you want to add an effect.
        Destroy(gameObject);
    }
}
