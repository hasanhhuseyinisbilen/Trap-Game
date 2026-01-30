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

    [Header("Ground Check")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheckTransform; // Optional override
    
    // Jump buffering
    float jumpBufferTime = 0.1f;
    float jumpBufferCounter;

    // Coyote time
    float coyoteTime = 0.1f;
    float coyoteTimeCounter;

    void OnEnable()
    {
        move.Enable();
        jump.Enable();
    }

    private Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>(); // Collider referansı al
        rb.freezeRotation = true;
    }

    void Update()
    {
        moveInput = move.ReadValue<float>();

        // Yön çevirme
        if (moveInput > 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < -0.1f)
            transform.localScale = new Vector3(-1, 1, 1);

        // --- GELİŞMİŞ ZEMİN KONTROLÜ (OverlapBox) ---
        if (groundLayer.value == 0) groundLayer = ~0;

        // Collider sınırlarını al (Pivotu önemsemez, gerçek boyutu kullanır)
        Bounds bounds = col.bounds;
        
        // Ayakların biraz aşağısında bir kutu oluştur
        float extraHeight = 0.1f;
        Vector2 boxCenter = new Vector2(bounds.center.x, bounds.min.y - extraHeight / 2);
        Vector2 boxSize = new Vector2(bounds.size.x * 0.9f, extraHeight);

        // Kutunun içinde ne var?
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0, groundLayer);
        
        isGrounded = false;
        foreach (var h in hits)
        {
            // Kendimiz değilse ve trigger değilse (opsiyonel) zemindir
            if (h.gameObject != gameObject && !h.isTrigger)
            {
                isGrounded = true;
                break;
            }
        }

        // Debug Çizgisi (Kutu çizimi Scene ekranında daha net görünür)
        Color debugColor = isGrounded ? Color.green : Color.red;
        // Basit çizim: Merkezden kenarlara
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y, 0), new Vector3(bounds.max.x, bounds.min.y, 0), debugColor);
        Debug.DrawLine(new Vector3(bounds.min.x, bounds.min.y - extraHeight, 0), new Vector3(bounds.max.x, bounds.min.y - extraHeight, 0), debugColor);
        
        // Coyote Time ve Buffer
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (jump.WasPressedThisFrame())
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Zıplama Mantığı:
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Debug.Log("Jumping! Force Applied."); // FİZİK KONTROLÜ
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpBufferCounter = 0f;
            coyoteTimeCounter = 0f; 
        }
        else if (jumpBufferCounter > 0f)
        {
             // Tuşa basıldı ama zıplamıyorsa nedenini yaz
             Debug.Log($"Jump Failed. Grounded: {isGrounded}, Coyote: {coyoteTimeCounter}");
        }
    }
}
