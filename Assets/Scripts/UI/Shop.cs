
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    int diamond;
    public TMP_Text diamondText;
    public TMP_Text skillsText;
    public void Start() {
        
        UpdateText();
    }
    public void BuySkill() {
        Debug.Log("BuySkill");
        if (diamond >= 5) {
            diamond -= 5;
            PlayerPrefs.SetInt("Diamond", diamond);
            PlayerPrefs.SetInt("Skills", PlayerPrefs.GetInt("Skills") + 1);

            UpdateText();
        }
        
    }
    public void BuyHealth() {
         Debug.Log("BuyHealth");
        if (diamond >= 2) {
            diamond -= 2;
            PlayerPrefs.SetInt("Diamond", diamond);
            GameManager.ModifyPlayerHealth(3);
            UpdateText();
        }
    }
    public void BuyCoin() {
        Debug.Log("BuyCoin");
        if (diamond >= 2) {
            diamond -= 2;
            PlayerPrefs.SetInt("Diamond", diamond);
            GameManager.ModifyGold(20);
            UpdateText();
        }
        
    }
    private void UpdateText() {
        diamond = PlayerPrefs.GetInt("Diamond");
        int skills = PlayerPrefs.GetInt("Skills");
        diamondText.text = diamond.ToString();
        skillsText.text = skills.ToString();
        
    }
}