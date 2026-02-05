using UnityEngine;

public class BuzSarkiti : MonoBehaviour
{
    private Vector3 startPos;
    private bool hasFallen = false;
    public float minFallDistance = 0.5f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Eğer henüz 'düştü' olarak işaretlenmediyse ve başlangıçtan aşağı doğru minFallDistance kadar kaydıysa
        if (!hasFallen && (startPos.y - transform.position.y) > minFallDistance)
        {
            hasFallen = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Sadece belirli bir mesafe düştükten sonra çarptığı herhangi bir şeyi siler
        if (hasFallen)
        {
            Destroy(gameObject);
        }
    }
}
