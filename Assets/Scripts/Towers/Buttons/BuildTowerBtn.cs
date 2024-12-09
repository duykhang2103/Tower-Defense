using UnityEngine;

public class BuildTowerBtn : MonoBehaviour {
  public GameObject openSpot;
  public string towerType;

  private void OnMouseDown() {
    if (openSpot != null) {
      openSpot.GetComponent<OpenSpot>().BuildTower(towerType);
      
    }
  }

  private void OnMouseUp() {
  }
}