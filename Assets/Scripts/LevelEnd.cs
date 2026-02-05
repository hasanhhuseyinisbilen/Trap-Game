using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçişi için eklendi

public class LevelEnd : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        SnowyLevelManager manager = FindObjectOfType<SnowyLevelManager>();
        if (manager != null)
        {
            // 1. İlerlemeyi Kaydet
            int currentIdx = LevelSelector.SelectedLevelIndex; // Oynanan level
            int reached = PlayerPrefs.GetInt("ReachedLevel", 0); // Kayıtlı olan en yüksek level

            // Eğer şu anki leveli ilk kez bitiriyorsak, bir sonrakini aç
            if (currentIdx == reached)
            {
                PlayerPrefs.SetInt("ReachedLevel", reached + 1);
                PlayerPrefs.Save(); // Veriyi fiziksel olarak kaydet
                Debug.Log("TEBRİKLER! Yeni bölüm açıldı: " + (reached + 2));
            }

            // 2. Sahne Değiştir (Level Seçme Sahnesi - İndeks 1)
            Debug.Log("Bölüm Bitti, Seçim Ekranına Dönülüyor...");
            SceneManager.LoadScene(1); 
        }
    }
}
