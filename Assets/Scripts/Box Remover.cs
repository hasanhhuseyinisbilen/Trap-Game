using UnityEngine;

public class BoxRemover : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Eğer çarpan objenin tag'i Player ise
        if (collision.gameObject.CompareTag("Player"))
        {
            // Sprite Renderer'ı sil (Görünmez olur)
            if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            {
                Destroy(sr);
            }

            // Box Collider 2D'yi sil (İçinden geçilebilir olur)
            if (TryGetComponent<BoxCollider2D>(out BoxCollider2D bc))
            {
                Destroy(bc);
            }
            
            // Not: Eğer objeyi komple silmek istersen sadece Destroy(gameObject); yeterliydi.
            // Ama bu yöntemle objenin kendisi (ve varsa içindeki scriptler) çalışmaya devam eder.
        }
    }
}