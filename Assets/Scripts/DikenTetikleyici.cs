using UnityEngine;

public class DikenTetikleyici : MonoBehaviour
{
    [Header("Settings")]
    public DusenDiken targetSpike; // The spike object that will fall

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            
            if (targetSpike != null)
            {
                targetSpike.StartFalling();
            }
            else
            {
                Debug.LogWarning("DikenTetikleyici: Target Spike not assigned!");
            }
        }
    }
}
