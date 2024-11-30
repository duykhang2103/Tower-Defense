
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public GameObject sceneUIManager;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fpsDropdown;
    public int width = 1080;
    public int height = 1920;
    public int fps = 60;
    public void Start() {
        width = PlayerPrefs.GetInt("WidthRes", 1080); 
        height = PlayerPrefs.GetInt("HeightRes", 1920);
        fps = PlayerPrefs.GetInt("FPS", 60);

        UpdateDropdown(resolutionDropdown, $"{width} x {height}");
        UpdateDropdown(fpsDropdown, fps.ToString());
    }
    public void UpdateResolution() {
       string resolution = resolutionDropdown.captionText.text;
        Debug.Log("resolution " + resolution);
        string[] dimensions = resolution.Split('x');
        width = int.Parse(dimensions[0].Trim());
        height = int.Parse(dimensions[1].Trim());
       
    }
    public void UpdateFPS() {
        string fpsString = fpsDropdown.captionText.text;
        fps = int.Parse(fpsString.Trim());
    }
    public void ConfirmUpdate() {
        PlayerPrefs.SetInt("FPS", fps);
        PlayerPrefs.SetInt("WidthRes", width);
        PlayerPrefs.SetInt("HeightRes", height);
        Debug.Log(fps + " " + width + " " + height);
        gameObject.SetActive(false); 
        PlayerPrefs.Save();
        sceneUIManager.GetComponent<HomeScene>().UpdateSetting();

    }
    private void UpdateDropdown(TMP_Dropdown dropdown, string value)
    {
        int index = dropdown.options.FindIndex(option => option.text == value);
        if (index != -1)
        {
            dropdown.value = index;
            dropdown.RefreshShownValue(); 
        }
    }
   
}