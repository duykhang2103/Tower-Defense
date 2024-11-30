using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MapScene : MonoBehaviour
{
    public void Start() {
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
                letterBoxer.width = PlayerPrefs.GetInt("WidthRes");
                letterBoxer.height = PlayerPrefs.GetInt("HeightRes");
                Debug.Log("width " + PlayerPrefs.GetInt("WidthRes"));
                Debug.Log("height " + PlayerPrefs.GetInt("HeightRes"));
                if (letterBoxer.width == 0) letterBoxer.width = 1080;
                if (letterBoxer.height == 0) letterBoxer.height = 1920;
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
        Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
        if (Application.targetFrameRate == 0)  Application.targetFrameRate = 30;

    }
}
