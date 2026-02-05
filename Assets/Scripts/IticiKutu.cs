using UnityEngine;

public class IticiKutu : MonoBehaviour
{
    [Header("Settings")]
    public float pushSpeed = 15f;
    public float maxDistance = 5f;

    private Vector3 startPosition;
    private bool active = false;
    private SpriteRenderer sr;

    void Start()
    {
        startPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();

        // Make invisible at start
        if (sr != null)
        {
            sr.enabled = false;
        }
    }

    public void Activate()
    {
        if (active) return;
        active = true;

        // Make visible when activated
        if (sr != null)
        {
            sr.enabled = true;
        }

        Debug.Log("IticiKutu: Activated and made visible!");
    }

    void Update()
    {
        if (active)
        {
            // Move the box to the left
            transform.position += Vector3.left * pushSpeed * Time.deltaTime;

            // Distance check to prevent the box from going infinitely
            if (Vector3.Distance(startPosition, transform.position) > maxDistance)
            {
                Destroy(gameObject); // Delete the box when it reaches max distance
            }
        }
    }
}
