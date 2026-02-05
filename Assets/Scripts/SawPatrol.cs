using UnityEngine;

public class SawPatrol : MonoBehaviour
{
    [Header("Ayarlar")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 360f;
    
    [Header("Hareket Mesafesi")]
    [Tooltip("Başlangıç noktasından ne kadar sola gidebilir?")]
    [SerializeField] private float leftDistance = 3f;
    
    [Tooltip("Başlangıç noktasından ne kadar sağa gidebilir?")]
    [SerializeField] private float rightDistance = 3f;

    private float leftLimit;
    private float rightLimit;
    private bool movingRight = true;

    void Start()
    {
        leftLimit = transform.position.x - leftDistance;
        rightLimit = transform.position.x + rightDistance;

        // Asansörlerle çarpışmayı yoksay (İçinden geçsin)
        Elevator[] elevators = FindObjectsOfType<Elevator>();
        Collider2D myCol = GetComponent<Collider2D>();

        if (myCol != null)
        {
            foreach (var elevator in elevators)
            {
                Collider2D elevatorCol = elevator.GetComponent<Collider2D>();
                if (elevatorCol != null)
                {
                    Physics2D.IgnoreCollision(myCol, elevatorCol);
                }
            }
        }
    }

    void Update()
    {

        transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);

    
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            
         
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }
}
