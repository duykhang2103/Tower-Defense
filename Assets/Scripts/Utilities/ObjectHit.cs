using UnityEngine;

public class ObjectHit : MonoBehaviour {
  public TutorialManager tutorialManager;
  public int stepToTrigger;
  public string textToDisplay;
  private void OnMouseDown() {
    if (tutorialManager.curStep == stepToTrigger) {
      tutorialManager.curStep++;
      tutorialManager.displayText(textToDisplay);
    }
  }
}