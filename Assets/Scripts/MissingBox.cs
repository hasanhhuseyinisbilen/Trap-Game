using UnityEngine;

public class BoxDisappearTrigger : MonoBehaviour
{
    [SerializeField] private GameObject boxObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Sprite kapat
        SpriteRenderer sr = boxObject.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.enabled = false;

        // TÜM collider'ları kapat
        foreach (Collider2D col in boxObject.GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
    }
}
