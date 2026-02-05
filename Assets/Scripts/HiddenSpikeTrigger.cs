using UnityEngine;

public class HiddenSpikeTrigger : MonoBehaviour
{
    [Header("Hedef Diken (Spike)")]
    [SerializeField] private GameObject targetSpike;

    private SpriteRenderer spikeRenderer;
    private PolygonCollider2D spikeCollider;
    private bool activated = false;

    void Start()
    {
        if (targetSpike != null)
        {
            spikeRenderer = targetSpike.GetComponent<SpriteRenderer>();
            spikeCollider = targetSpike.GetComponent<PolygonCollider2D>();

            if (spikeRenderer) spikeRenderer.enabled = false;
            if (spikeCollider) spikeCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;
        if (other.CompareTag("Player"))
        {
            activated = true;


            if (spikeRenderer) spikeRenderer.enabled = true;
            if (spikeCollider) spikeCollider.enabled = true;
            
        }
    }
}
