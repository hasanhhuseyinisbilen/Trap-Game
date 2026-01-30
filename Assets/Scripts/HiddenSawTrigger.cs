using UnityEngine;

public class HiddenSawTrigger : MonoBehaviour
{
    [Header("Gizli Obje (Testere)")]
    [SerializeField] private GameObject targetSaw;

    private SpriteRenderer sawRenderer;
    private Collider2D sawCollider;
    private SawMoveLeft sawMovement;
    private bool activated = false;

    void Start()
    {
        if (targetSaw != null)
        {
            sawRenderer = targetSaw.GetComponent<SpriteRenderer>();
            sawCollider = targetSaw.GetComponent<Collider2D>();
            sawMovement = targetSaw.GetComponent<SawMoveLeft>();

            // Başlangıçta gizle
            if (sawRenderer) sawRenderer.enabled = false;
            if (sawCollider) sawCollider.enabled = false;
            // Hareketi durdur (Script üzerindeki public değişkeni kapatmıyoruz, scriptte 'move' bool'u var)
            if (sawMovement) sawMovement.move = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (other.CompareTag("Player"))
        {
            activated = true;

            // Görünür yap ve aktif et
            if (sawRenderer) sawRenderer.enabled = true;
            if (sawCollider) sawCollider.enabled = true;
            
            // Hareketi başlat
            if (sawMovement) sawMovement.move = true;

            Debug.Log("Hidden Saw Activated and Moving!");
        }
    }
}
