using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; // Butonları kontrol etmek için eklendi

public class LevelSelector : MonoBehaviour
{
    [Header("Sahne Ayarları")]
    public string gameSceneName = "GameScene"; 

    [Header("Bölüm Kilit Sistemi")]
    [Tooltip("Level butonlarını sırasıyla (Bölüm 1, 2, 3...) buraya sürükle.")]
    public Button[] levelButtons; 

    public static int SelectedLevelIndex = 0;

    void Start()
    {
        // Daha önce hiçbir buton atanmamışsa hata vermemesi için kontrol
        if (levelButtons == null || levelButtons.Length == 0) return;

        // Kayıtlı ilerlemeyi çek (Varsayılan: 0, yani sadece 1. bölüm açık)
        int reachedLevel = PlayerPrefs.GetInt("ReachedLevel", 0);

        Debug.Log("Sistem Başlatıldı. Mevcut İlerleme: " + (reachedLevel + 1) + ". Bölüm");

        // Butonları kilitle/aç
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= reachedLevel)
            {
                levelButtons[i].interactable = true; // Açık
            }
            else
            {
                levelButtons[i].interactable = false; // Kilitli
            }
        }
    }

    public void SelectLevel(int levelID)
    {
        Debug.Log("Butona basıldı! Seçilen Level ID: " + levelID);
        SelectedLevelIndex = levelID;

        if (!string.IsNullOrEmpty(gameSceneName))
        {
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            SceneManager.LoadScene(2); 
        }
    }

    // İlerlemeyi sıfırlamak istersen bu kodu bir butona atayabilirsin
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("ReachedLevel");
        Debug.Log("İlerleme sıfırlandı!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
