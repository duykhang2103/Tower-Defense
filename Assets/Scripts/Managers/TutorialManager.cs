using System.Net.Http.Headers;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
  public int curStep;
  public string textToDisplay;

  public string[] targetsToHit = {"Banner", "OpenSpotHitBox", "ShootingTowerBtn", "OpenSpotHitBox", "SummoningTowerBtn"};
  public string[] textsToDisplay = {
    "Welcome to the tutorial! Click on the banner to continue.",
    "Great! Now click on the open spot to place a tower.",
    "Awesome! Now click on the shooting tower button.",
    "Nice! Now click on the open spot to place a tower.",
    "Well done! Now click on the summoning tower button."
  };

  public ClickedDetector clickedDetector;

  public GameObject[] hitClicksToContinue;

  void Start() {
    curStep = 0;
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      string targetName = clickedDetector.getObjectHitName();
      if (targetName == targetsToHit[curStep]) {
        if (curStep < hitClicksToContinue.Length - 1) {
          // hitClicksToContinue[curStep].SetActive(true);
        curStep++;
        }
      }
    }
  }

  public string getCurDisplayText() {
    return textsToDisplay[curStep];
  }
}