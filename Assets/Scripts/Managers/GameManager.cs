using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //singleton

    public WaveManager waveManager;
    public GameObject startWaveBtn;

    public TextMeshProUGUI healthText; 
    public TextMeshProUGUI goldText; 
    private int playerHealth = 15;
    private int gold = 100;

    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
    }

    public void Start()
    {
        playerHealth = 15;
        gold = 100;
        UpdateHealthText();
        UpdateGoldText();
        startWaveBtn.SetActive(true);
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

    public void OnStartWaveButtonClicked()
    {
        waveManager.StartWave();
        startWaveBtn.SetActive(false);
        StartCoroutine(WaitForWaveEnd());
    }

    private IEnumerator WaitForWaveEnd()
    {
        yield return new WaitUntil(() => waveManager.IsWaveFinished);
        if (waveManager.noMoreWaves) yield break;
        startWaveBtn.SetActive(true);
    }
}
