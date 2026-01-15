using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public InputAction move;
    public InputAction jump;

    Rigidbody2D rb;

    float moveInput;
    bool jumpRequested;
    bool isGrounded;

    void OnEnable()
    {
        move.Enable();
        jump.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
          rb.freezeRotation = true; // 🔴 KRİTİK SATIR
    }

    void Update()
    {
        moveInput = move.ReadValue<float>();

        // Yön çevirme (SİLİNMEYECEK)
        if (moveInput > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (jump.WasPressedThisFrame())
            jumpRequested = true;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (jumpRequested && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        jumpRequested = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // YALNIZCA ALT TEMAS
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
