using UnityEngine;

public class SnowyLevelManager : MonoBehaviour
{
    public static SnowyLevelManager Instance { get; private set; }

    [System.Serializable]
    public class LevelContent
    {
        public string levelName;

        public GameObject[] environmentPrefabs;
        public Vector3[] environmentPositions;

        public GameObject[] traps;
        public Vector3[] trapPositions;
    }

    [Header("Level Veritabanı")]
    [SerializeField] private LevelContent[] allLevels;

    [Header("Ayarlar")]
    [SerializeField] private int currentLevelIndex = 0;

    private GameObject levelRoot;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // LevelSelector'dan gelen seçilen level numarasını al
        currentLevelIndex = LevelSelector.SelectedLevelIndex;
        Debug.Log("SnowyLevelManager Başlatıldı. Yüklenecek Level Index: " + currentLevelIndex);
        BuildLevel(currentLevelIndex);
    }

    public void BuildLevel(int index)
    {
        if (allLevels == null || allLevels.Length == 0)
        {
            Debug.LogError("HATA: allLevels dizisi boş! Lütfen Inspector'dan levelleri ekleyin.");
            return;
        }

        if (index < 0 || index >= allLevels.Length)
        {
            Debug.LogError("HATA: Geçersiz level indeksi: " + index + ". Dizi boyutu: " + allLevels.Length);
            return;
        }

        Debug.Log("Level oluşturuluyor: " + allLevels[index].levelName);

        // Eski seviyeyi temizle
        if (levelRoot != null) Destroy(levelRoot);
        levelRoot = new GameObject("LevelRoot_" + index);

        LevelContent level = allLevels[index];

        // Çevreyi oluştur
        if (level.environmentPrefabs != null)
        {
            for (int i = 0; i < level.environmentPrefabs.Length; i++)
            {
                if (i < level.environmentPositions.Length && level.environmentPrefabs[i] != null)
                {
                    Instantiate(level.environmentPrefabs[i], level.environmentPositions[i], Quaternion.identity, levelRoot.transform);
                }
            }
        }

        // Tuzakları oluştur
        if (level.traps != null)
        {
            for (int i = 0; i < level.traps.Length; i++)
            {
                if (i < level.trapPositions.Length && level.traps[i] != null)
                {
                    Instantiate(level.traps[i], level.trapPositions[i], Quaternion.identity, levelRoot.transform);
                }
            }
        }
        
        // Oyuncuyu sıfırla
        ResetPlayer();
    }

    public void LoadSpecificLevel(int index)
    {
        if (allLevels == null || index < 0 || index >= allLevels.Length) return;

        // Canları fulle (Eğer LivesManager varsa)
        if (LivesManager.Instance != null) LivesManager.Instance.LoseLife(); // Örnek, aslında ResetLives olmalı ama undo yapmıştık.

        currentLevelIndex = index;
        BuildLevel(currentLevelIndex);
    }

    public void RestartLevel()
    {
        BuildLevel(currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (allLevels == null || allLevels.Length == 0) return;
        if (currentLevelIndex >= allLevels.Length) currentLevelIndex = 0;
        BuildLevel(currentLevelIndex);
    }

    private void ResetPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero; // linearVelocity kuralı
        }
    }
}
