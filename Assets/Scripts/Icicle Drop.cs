using UnityEngine;

public class IcicleDrop : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        Rigidbody2D[] allBodies = FindObjectsOfType<Rigidbody2D>();

        foreach (var rb in allBodies)
        {

            if (!rb.CompareTag("Trap"))
                continue;

            if (rb.bodyType == RigidbodyType2D.Dynamic)
                continue;

            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 10f;
            rb.linearVelocity = Vector2.zero;

            // Silinme kodunu otomatik ekle
            if (rb.gameObject.GetComponent<BuzSarkiti>() == null)
            {
                rb.gameObject.AddComponent<BuzSarkiti>();
            }
        }
    }
}
