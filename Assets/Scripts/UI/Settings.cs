
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fpsDropdown;
    public int width = 1080;
    public int height = 1920;
    public int fps = 30;
    public void Start() {
        PlayerPrefs.SetInt("WidthRes", 1080);
        PlayerPrefs.SetInt("HeightRes", 1920);
        PlayerPrefs.Save();
    }
    public void UpdateResolution() {
        string resolution = resolutionDropdown.captionText.text;
        Debug.Log("resolution " + resolution); 
        // resolution có dạng a x b 
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

    }
    public void Cancel () {
        gameObject.SetActive(false); 
    }
   
}