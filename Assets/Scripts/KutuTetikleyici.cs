using UnityEngine;

public class KutuTetikleyici : MonoBehaviour
{
    [Header("Target Box")]
    public IticiKutu targetBox;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("KutuTetikleyici: Player entered!");
            triggered = true;
            
            if (targetBox != null)
            {
                targetBox.Activate();
            }
            else
            {
                Debug.LogError("KutuTetikleyici: Target Box not assigned!");
            }
        }
    }
}
