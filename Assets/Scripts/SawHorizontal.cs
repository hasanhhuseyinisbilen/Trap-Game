using UnityEngine;

public class SawMoveLeft : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;


    [HideInInspector] public bool move;

    void Update()
    {
        if (!move) return;

        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);
    }
}
