using UnityEngine;

public class TavanTetikleyici : MonoBehaviour
{
    [Header("Ceiling Group")]
    public GameObject ceilingParent; // Parent object containing all ceiling blocks
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            
            if (ceilingParent != null)
            {
                // Find all TavanBlogu components in children and make them fall
                TavanBlogu[] pieces = ceilingParent.GetComponentsInChildren<TavanBlogu>();
                foreach (var piece in pieces)
                {
                    piece.StartFalling();
                }
            }
        }
    }
}
