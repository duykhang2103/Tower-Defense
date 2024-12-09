using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class TutorialManager : MonoBehaviour {
    public int curStep = 0;
    public TextMeshProUGUI descriptionText;


    public GameObject openSpot1;
    public GameObject openSpot2;
    public GameObject shootTower;
    public GameObject summonTower;
    public GameObject soldier;
    public GameObject StartWaveBtn;
    public bool isFocusTower = false;

    private string[] textsToDisplay = {
        "This is the tutorial how to play game",
        "This is a tower spot. Click on it to build a tower.",
        "This is the shooting tower button. Click on it to build a shooting tower.",
        "This is another tower spot. Click on it to build a tower.",
        "This is the summoning tower button. Click on it to build a summoning tower.",
        "Click on this summoning tower and chosse either archer or warrior",
        "When the soldier appears, click a position within the tower to move your soldier",
        "Click the tower again to turn off focus",
        "Now click here to start the monster spawn.",
        "Click BACK button on the top left corner to leave tutorial"
    };
    public void UpdateStep(int StepIdx)
    {
        if (StepIdx != curStep) return;

        curStep++;
        switch (curStep)
        {
            case 1:
                openSpot1.SetActive(true);
                openSpot1.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                break;
            case 2:
                SpriteRenderer[] allSprites1 = openSpot1.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in allSprites1)
                {
                    spriteRenderer.sortingLayerName = "UI"; 
                }
                break;

            case 3:
                shootTower.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
                openSpot2.SetActive(true);
                SpriteRenderer[] allSprites2 = openSpot2.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in allSprites2)
                {
                    spriteRenderer.sortingLayerName = "UI";
                }
                break;
            case 4:  
                break;
            case 5:
                Debug.Log("chay toi day");
                summonTower.GetComponentInChildren<Canvas>().sortingLayerName = "UI";
                break;
            case 6:
                SpriteRenderer[] allSprites3 = soldier.GetComponentsInChildren<SpriteRenderer>();
                foreach (SpriteRenderer spriteRenderer in allSprites3)
                {
                    spriteRenderer.sortingLayerName = "UI";
                }
                Invoke("DelayForNextStep", 3f);
                break;
            case 8:
                GameObject.Find("Panel").SetActive(false);
                Invoke("DelayForNextStep", 5f);
                break;
            case 9:
                
                break;

        }
        
        
    }
    private void Update()
    {
        UpdateDescription();
        
        if (shootTower == null)
        {
            shootTower = GameObject.Find("ShootingTower(Clone)");
            if (shootTower != null) UpdateStep(2);
        }
        if (summonTower == null)
        {
            summonTower = GameObject.Find("Tower(Clone)");
            if (summonTower != null) UpdateStep(4);
        }
        if (soldier == null)
        {
            soldier = GameObject.FindGameObjectWithTag("soldier");
            if (soldier != null) UpdateStep(5);
        }
        if (!isFocusTower && summonTower != null)
        {
            isFocusTower = (isFocusTower || summonTower.GetComponent<Tower>().isFocused); 
            SpriteRenderer[] allSprites = summonTower.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in allSprites)
            {
                spriteRenderer.sortingLayerName = "UI";
            }
 
        }
        if (curStep == 7)
        {
            if (summonTower.GetComponent<Tower>().isFocused == false) UpdateStep(7);
        }
        if (curStep < 8) StartWaveBtn.SetActive(false);


    }
    private void Start()
    {
        UpdateDescription();
        Invoke("DelayForNextStep", 2.5f);
        
    }
    private void DelayForNextStep()
    {
        
        UpdateStep(curStep);
        //curStep++;
    }
    void UpdateDescription()
    {
        descriptionText.text = textsToDisplay[curStep];
    }
}