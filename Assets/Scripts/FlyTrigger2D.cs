using UnityEngine;

public class FlyTrigger2D : MonoBehaviour
{
    public float speed = 10f;
    private GameObject[] boxesToFly;
    private bool shouldFly = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            boxesToFly = GameObject.FindGameObjectsWithTag("FlyBox");
            if (boxesToFly.Length > 0)
            {
                shouldFly = true;
                foreach (GameObject box in boxesToFly)
                {
                    if (box.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                    {
                        rb.simulated = false;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (shouldFly && boxesToFly != null)
        {
            foreach (GameObject box in boxesToFly)
            {
                if (box != null)
                {
                    box.transform.position += Vector3.up * speed * Time.deltaTime;
                }
            }
        }
    }
}
