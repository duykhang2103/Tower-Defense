using UnityEngine;

public class TutorialBanner : MonoBehaviour {
  public TutorialManager tutorialManager;

  private void OnMouseDown() {
    Debug.Log("Banner Clicked!");
  }
}