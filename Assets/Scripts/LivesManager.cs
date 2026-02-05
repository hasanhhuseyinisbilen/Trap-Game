using UnityEngine;
using TMPro; 

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance { get; private set; }

    [Header("Can Ayarları")]
    [SerializeField] private int maxLives = 3;
    private int currentLives;

    [Header("UI Referansları")]
    [SerializeField] private TextMeshProUGUI livesText; // TextMeshPro UI Text componenti
    [SerializeField] private GameObject gameOverPanel; // Game Over panel (opsiyonel)
    [SerializeField] private TextMeshProUGUI gameOverText; 

    private bool isProcessingDeath = false; // Ölüm işlemi devam ediyor mu?
    private float lastDeathTime = -999f; // Son ölüm zamanı 

    void Awake()
    {
  
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLives = maxLives;
        UpdateUI();

        // Game Over UI'ı başlangıçta gizle
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
            Debug.Log("GameOverText başlangıçta gizlendi.");
        }
        else
        {
            Debug.LogWarning("GameOverText atanmadı! Lütfen Inspector'dan GameOverText'i bağlayın.");
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void LoseLife()
    {
        // Eğer son ölümden bu yana 0.3 saniye geçmediyse ignore et
        if (Time.time - lastDeathTime < 0.3f)
        {
            Debug.LogWarning("Çok hızlı ölüm girişimi engellendi!");
            return;
        }

        // Eğer zaten game over ise ignore et
        if (isProcessingDeath)
        {
            Debug.LogWarning("Zaten ölüm işlemi devam ediyor!");
            return;
        }

        lastDeathTime = Time.time; // Son ölüm zamanını güncelle

        currentLives--;
        Debug.Log($"Can kaybedildi! Kalan can: {currentLives}");
        
        if (currentLives < 0)
            currentLives = 0;

        UpdateUI();

        if (currentLives <= 0)
        {
           GameOver();
        }
    }

    void UpdateUI()
    {
        if (livesText != null)
        {
            livesText.text = "Health: " + currentLives;
        }
    }

    void GameOver()
    {

    
        // Game Over UI'ı göster
        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER";
            gameOverText.gameObject.SetActive(true);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 2 saniye sonra restart
        Invoke("ResetGame", 5f);
    }

    void ResetGame()
    {
     
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Canları resetle
        currentLives = maxLives;
        UpdateUI();

        // Level'ı restart et
        SnowyLevelManager manager = FindObjectOfType<SnowyLevelManager>();
        if (manager != null)
        {
            manager.RestartLevel();
        }
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
}
