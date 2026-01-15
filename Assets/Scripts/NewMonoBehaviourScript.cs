using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    private SpikeMover mover;

    void Awake()
    {
        mover = GetComponentInParent<SpikeMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            mover.StartMove();
    }
}
