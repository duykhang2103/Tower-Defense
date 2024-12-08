using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;  

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } 
    public string gameStageStr = "Stage_1";
    public WaveManager waveManager;
    public GameObject VictoryFrame;
    public GameObject DefeatFrame;
 
    public GameObject AchievementBoard;
    public TextMeshProUGUI healthText; 
    public TextMeshProUGUI goldText;

    public GameObject canvas;
    public int playerHealth = 15;
    public int gold = 100;
    private bool isWaveFinished = false;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this; 
        }
    }

    public void Start()
    {
        playerHealth = 15;
        gold = 100;
        UpdateHealthText();
        UpdateGoldText();
        VictoryFrame.SetActive(false);
        DefeatFrame.SetActive(false);
    }
    public void Update()
    {
        if (waveManager.IsWaveFinished)
        {
            isWaveFinished = true;
        }

        if (waveManager.noMoreWaves && isWaveFinished)
        {
            if (playerHealth <= 0)
            {
                Defeat();
            }
            else
            {
                Enemy[] enemies = FindObjectsOfType<Enemy>();
                if (enemies.Length == 0)
                {
                    Victory();
                }
            }
        }
        CheckAchievement();
    }
    public void CheckAchievement()
    {
        if (PlayerPrefs.GetInt("win all maps", 0) == 0)
        {
            
            if (PlayerPrefs.GetInt("Stage_1") > 0 && PlayerPrefs.GetInt("Stage_2") > 0)
            {
                PlayerPrefs.SetInt("win all maps", 1);
                GameObject achievementPanel = Instantiate(AchievementBoard, canvas.transform);
                achievementPanel.GetComponent<PopupAchievement>().Init("Win all maps");
            }
            
            
        }
        if (PlayerPrefs.GetInt("earns 20 diamonds", 0) == 0)
        {
            
            if (PlayerPrefs.GetInt("Diamonds") >= 20)
            {
                PlayerPrefs.SetInt("earns 20 diamonds", 1);
                GameObject achievementPanel = Instantiate(AchievementBoard, canvas.transform);
                achievementPanel.GetComponent<PopupAchievement>().Init("Earns 20 diamonds ");
            }
        }
        if (PlayerPrefs.GetInt("kills 10 enemies", 0) == 0)
        {
            
            if (PlayerPrefs.GetInt("Enemies") >= 10)
            {
                PlayerPrefs.SetInt("kills 10 enemies", 1);
                GameObject achievementPanel = Instantiate(AchievementBoard, canvas.transform);
                achievementPanel.GetComponent<PopupAchievement>().Init("Kills 10 enemies");
            }
        }
            
    }



    public void UpdateHealthText()
    {
        healthText.text = playerHealth.ToString();
    }

    public void UpdateGoldText()
    {
        goldText.text = gold.ToString();
    }

  
    public static void ModifyPlayerHealth(int amount)
    {
        if (Instance != null)
        {
            Instance.playerHealth += amount;
            Instance.playerHealth = Math.Max(Instance.playerHealth, 0);
            Instance.UpdateHealthText();
        }
    }

    public static void ModifyGold(int amount)
    {
        if (Instance != null)
        {
            Instance.gold += amount;
            Instance.UpdateGoldText();
        }
    }

    public static void Defeat()
    {
        if (Instance != null)
        {
            Instance.DefeatFrame.SetActive(true);
            PlayerPrefs.SetInt(Instance.gameStageStr, Instance.playerHealth);
        }
        
    }
    public static void Victory() {
        if (Instance != null)
        {
            Instance.VictoryFrame.SetActive(true);
            PlayerPrefs.SetInt(Instance.gameStageStr, Instance.playerHealth);
        }
    }

    public void onAchievementButtonClicked()
    {
        AchievementBoard.SetActive(true);
    }

    public void OnExitButtonClicked()
    {
        Instance.AchievementBoard.SetActive(false);
    }
}
