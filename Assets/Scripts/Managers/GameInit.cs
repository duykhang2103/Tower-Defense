using UnityEngine;

public class GameInit : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("GameInitialized"))
        {
            RunOnce();
            PlayerPrefs.SetInt("GameInitialized", 1); 
            PlayerPrefs.Save(); 
        }
    }

    void RunOnce()
    {
        PlayerPrefs.SetInt("Diamond", 10);
        PlayerPrefs.SetInt("Skills", 3);
        
    }
}
