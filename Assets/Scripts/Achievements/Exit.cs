using System;
using UnityEngine;

public class Exit : MonoBehaviour {
  public GameManager gameManager;
  public Boolean isVisible;

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