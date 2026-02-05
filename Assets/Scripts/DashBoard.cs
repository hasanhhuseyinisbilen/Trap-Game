using UnityEngine;
using UnityEngine.SceneManagement;

public class DashBoard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
