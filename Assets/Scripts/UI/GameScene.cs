using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameScene : MonoBehaviour
{
    public void Start () {
        Time.timeScale = 1f; 
        UpdateSetting();
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
                Debug.Log("width " + PlayerPrefs.GetInt("WidthRes"));
                Debug.Log("height " + PlayerPrefs.GetInt("HeightRes"));
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
    public void UpdateSetting() {
        UpdateResolution();
        UpdateAudio();
        UpdateFPS();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void PauseGame() {
        Time.timeScale = 0f;
        
    }
    public void ResumeGame() {
        Time.timeScale = 1f; 
    }
}

