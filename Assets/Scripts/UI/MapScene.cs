using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MapScene : MonoBehaviour
{
    public void Start() {
        Time.timeScale = 1f; 
        UpdateSetting();
    }
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    public void UpdateSetting() {
        UpdateResolution();
        UpdateAudio();
        UpdateFPS();
    }
    private void UpdateResolution() {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            LetterBoxer letterBoxer = mainCamera.GetComponent<LetterBoxer>();
            if (letterBoxer != null)
            {
                letterBoxer.width = PlayerPrefs.GetInt("WidthRes", 1080);
                letterBoxer.height = PlayerPrefs.GetInt("HeightRes", 1920);
            }
            else {
                Debug.Log("letter boxer not found");
            }
        }
    }
    private void UpdateAudio() {
        // AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1f);
    }
    private void UpdateFPS() {
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS", 60);
        

    }

    private void closeAchievement() {
        GameObject achievement = GameObject.Find("Achievement");
        if (achievement != null) {
            achievement.SetActive(false);
        }
    }
}
