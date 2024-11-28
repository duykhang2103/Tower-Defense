using UnityEngine;

public class Exit : MonoBehaviour {
  public GameManager gameManager;

  private void OnMouseDown() {
    Debug.Log("Exit Button Clicked!");
    gameManager.OnExitButtonClicked();
  }
}