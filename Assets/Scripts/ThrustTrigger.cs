using UnityEngine;

public class BoxCloseTrigger : MonoBehaviour
{
    [Header("Normal Boxlar")]
    [SerializeField] private SpriteRenderer[] boxSprites;
    [SerializeField] private  PolygonCollider2D[] polygonColliders;


    private bool triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (triggered) return;

        triggered = true;

        // Normal box sprite kapat
        foreach (var sr in boxSprites)
            if (sr) sr.enabled = false;

        // Normal box collider kapat
        foreach (var pc in polygonColliders)
            if (pc) pc.enabled = false;

    }
}