using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements; 

public class HomeScene : MonoBehaviour
{
    public GameObject setting;
    public void Start() {
        Time.timeScale = 1f; 
        UpdateSetting();
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Diamond", 10);
        PlayerPrefs.SetInt("Skills", 3);
        SceneManager.LoadScene("MapScene");
    }
    public void Continue()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void QuitGame()
    {
        Application.Quit();
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
}
