using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameScene : MonoBehaviour
{
    
    public void LoadGameScene()
    {
        Debug.Log("Button Pressed: Loading Game Scene");
        SceneManager.LoadScene("GameScene");
    }

  
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }

  
    public void LoadSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
