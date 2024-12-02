using UnityEngine;

public class Exit : MonoBehaviour {
  public GameManager gameManager;

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      gameManager.OnExitButtonClicked();
    }
  }

  private void OnMouseDown() {
    Debug.Log("Exit Button Clicked!");
    gameManager.OnExitButtonClicked();
  }
}