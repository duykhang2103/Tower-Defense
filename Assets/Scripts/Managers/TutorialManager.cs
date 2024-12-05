using UnityEngine;

public class TutorialManager : MonoBehaviour {
  public int curStep;
  private string[] objectsToHit = { "TextBanner", "OpenSpot_1", "ShootingTowerBtn", "OpenSpot_2", "SummoningTowerBtn", };
  private string[] textsToDisplay = {
    // "Welcome to the tutorial! CLick on this banner to continue.",
    "This is a tower spot. Click on it to build a tower.",
    "This is the shooting tower button. Click on it to build a shooting tower.",
    "This is another tower spot. Click on it to build a tower.",
    "This is the summoning tower button. Click on it to build a summoning tower.",
  };

  public ClickedDetector clickedDetector;

  void Start() {
    curStep = 0;
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
    Debug.Log(curStep);
    Debug.Log(clickedDetector.GetClickedObjectName());
      if (clickedDetector.GetClickedObjectName() == objectsToHit[curStep]) {
        GameObject textBanner = GameObject.Find("TextBanner");
        // Debug.Log(textBanner);
        // Canvas canvas = textBanner.Find("Canvas");
        GameObject textMesh = GameObject.Find("TextMesh");
        // Debug.Log(textMesh);
        textMesh.GetComponent<TMPro.TextMeshProUGUI>().text = textsToDisplay[curStep];
        // canvas.GetComponent<TMPro.TextMeshProUGUI>().text = textsToDisplay[curStep];
        // textBanner.GetComponent<TMPro.TextMeshProUGUI>().text = textsToDisplay[curStep];
        curStep++;
      }

    }
  }
}