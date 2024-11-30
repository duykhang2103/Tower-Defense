
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    int diamond;
    public TMP_Text diamondText;
    public void Start() {
        diamond = PlayerPrefs.GetInt("Diamond");
    }
    public void BuySkill() {
        if (diamond >= 5) {
            diamond -= 5;
            PlayerPrefs.SetInt("Diamond", diamond);
            PlayerPrefs.SetInt("Skills", PlayerPrefs.GetInt("Skills") + 1);
            UpdateText();
        }
        
    }
    public void BuyHealth() {
        diamond -= 2;
        PlayerPrefs.SetInt("Diamond", diamond);
        GameManager.ModifyPlayerHealth(3);
        UpdateText();
    }
    public void BuyCoin() {
        diamond -= 2;
        PlayerPrefs.SetInt("Diamond", diamond);
        GameManager.ModifyPlayerHealth(20);
        UpdateText();
    }
    private void UpdateText() {
        diamondText.text = diamond.ToString();
    }
}