using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private string volumePrefKey = "volume";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void SaveVolume(float volume)
    {
        PlayerPrefs.SetFloat(volumePrefKey, volume);
    }

    public float GetSavedVolume()
    {
        if (PlayerPrefs.HasKey(volumePrefKey))
        {
            return PlayerPrefs.GetFloat(volumePrefKey);
        }
        else
        {
            return 0.5f;
        }
    }
}
