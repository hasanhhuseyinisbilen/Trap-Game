using UnityEngine;

public class IcicleTrigger : MonoBehaviour
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
            
            if (!rb.CompareTag("Icicle"))
                continue;

            if (rb.bodyType == RigidbodyType2D.Dynamic)
                continue;

            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 10f;
            rb.linearVelocity = Vector2.zero;

            // Silinme kodunu otomatik ekle (böylece tek tek uğraşmazsınız)
            if (rb.gameObject.GetComponent<BuzSarkiti>() == null)
            {
                rb.gameObject.AddComponent<BuzSarkiti>();
            }

            // ROTASYON KİLİDİ
            rb.freezeRotation = true;
        }
    }
}
