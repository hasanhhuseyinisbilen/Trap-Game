using UnityEngine;

public class SnowBallTrigger : MonoBehaviour
{
    [Header("Target Object")]
    public Transform snowBall;   // Inspector’dan atanacak

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = -360f;

    private bool move = false;

    private void Update()
    {
        if (!move || snowBall == null) return;

        // Sola doğru hareket
        snowBall.Translate(Vector2.left * moveSpeed * Time.deltaTime, Space.World);

        // Z ekseninde dönme
        snowBall.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        move = true;
    }
}
