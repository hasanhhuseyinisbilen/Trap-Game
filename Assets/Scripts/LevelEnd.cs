using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private bool triggered = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
  

        if (triggered) 
        {
            return;
        }

        if (!other.CompareTag("Player")) 
        {
     
            return;
        }

        triggered = true;

        SnowyLevelManager manager = FindObjectOfType<SnowyLevelManager>();
        if (manager != null)
        {
            
            manager.LoadNextLevel();

     
            other.transform.position = Vector3.zero;

            // Fiziksel hızı sıfırla ki oyuncu kaymasın
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }
}
