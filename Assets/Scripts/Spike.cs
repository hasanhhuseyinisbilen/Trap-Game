using UnityEngine;

public class SpikeMover : MonoBehaviour
{
    public float speed = 10f;
    private bool move;

    public void StartMove()
    {
        move = true;
    }

    void Update()
    {
        if (!move) return;
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
