using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Ayarlar")]
    public float speed = 3f;
    public float targetY = 50f;
    public bool stopAtTarget = true;

    private bool isMoving = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
   
        if (rb) rb.gravityScale = 0;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            if (stopAtTarget && transform.position.y >= targetY)
            {
                rb.linearVelocity = Vector2.zero;
                isMoving = false;
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            }
            else
            {
                rb.linearVelocity = Vector2.up * speed;
            }
        }
        else
        {
          
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = true;           
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = false;
        }
    }
}
