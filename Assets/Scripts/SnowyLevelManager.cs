using UnityEngine;

public class SnowyLevelManager : MonoBehaviour
{
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

    void Start()
    {
        BuildLevel(currentLevelIndex);
    }

    public void BuildLevel(int index)
    {
        if (index < 0 || index >= allLevels.Length)
            return;

        LevelContent level = allLevels[index];

        if (levelRoot != null)
            Destroy(levelRoot);

        levelRoot = new GameObject("Level_" + index);

        // ENVIRONMENT
        for (int i = 0; i < level.environmentPrefabs.Length; i++)
        {
            if (i >= level.environmentPositions.Length) continue;
            if (level.environmentPrefabs[i] == null) continue;

            Instantiate(
                level.environmentPrefabs[i],
                level.environmentPositions[i],
                Quaternion.identity,
                levelRoot.transform
            );
        }

        // TRAPS
        for (int i = 0; i < level.traps.Length; i++)
        {
            if (i >= level.trapPositions.Length) continue;
            if (level.traps[i] == null) continue;

            Instantiate(
                level.traps[i],
                level.trapPositions[i],
                Quaternion.identity,
                levelRoot.transform
            );
        }
    }
    public void LoadNextLevel()
    {
        Debug.Log($"LoadNextLevel CALLED. Current Index before increase: {currentLevelIndex}, Total Levels: {allLevels.Length}");
        
        currentLevelIndex++;

        // Eğer son level bittiyse başa dön (Döngü)
        if (currentLevelIndex >= allLevels.Length)
        {
            Debug.Log("Last level reached. Looping back to Level 0 (Index 0).");
            currentLevelIndex = 0; // Başa sar
        }

        Debug.Log($"Attempting to build level index: {currentLevelIndex}");
        BuildLevel(currentLevelIndex);
    }

    public void RestartLevel()
    {
        BuildLevel(currentLevelIndex);
    }
}
