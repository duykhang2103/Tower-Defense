using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public WaveManager waveManager;
    public GameObject startWaveBtn;
    
    public static TextMeshProUGUI healthText; 
    public static TextMeshProUGUI goldText; 
    private static int playerHealth = 15;
    private static int gold = 100;

    private void Start()
    {
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>(); // Tìm kiếm đối tượng Text trên scene
        goldText = GameObject.Find("GoldText").GetComponent<TextMeshProUGUI>();     // Tìm kiếm đối tượng Text trên scene
        startWaveBtn.SetActive(true); 
        UpdateHealthText();
        UpdateGoldText();
    }

    public static void UpdateHealthText()
    {
        healthText.text = playerHealth.ToString();
    }

    public static void UpdateGoldText()
    {
        goldText.text = gold.ToString();
    }
    public static void ModifyPlayerHealth(int amount)
    {
        playerHealth += amount;
        UpdateHealthText(); 
    }

    public static void ModifyGold(int amount)
    {
        gold += amount;
        UpdateGoldText();
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
