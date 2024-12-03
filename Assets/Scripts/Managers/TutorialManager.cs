using UnityEngine;

public class TutorialManager : MonoBehaviour {
  public int curStep;
  public GameObject[] objectsToHit;
  public string textToDisplay;

  public GameObject[] hitClicksToContinue;

  void Start() {
    curStep = 0;
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Space)) {
      curStep++;
    }
  }

  public void displayText(string text) {
    textToDisplay = text;
  }
}