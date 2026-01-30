using UnityEngine;

public class SawMoveLeft : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;
    [Header("Dead Zone")]
    [SerializeField] private float deadZoneX = -50f; // Varsayılan yok oluş noktası
    [SerializeField] private bool useDeadZone = true;

    [HideInInspector] public bool move;

    void Update()
    {
        if (!move) return;

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);

        // Dead Zone Kontrolü
        if (useDeadZone && transform.position.x < deadZoneX)
        {
            Destroy(gameObject);
        }
    }
}
