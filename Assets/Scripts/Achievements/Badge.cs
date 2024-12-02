using UnityEngine;

public class Badge : MonoBehaviour {
  public GameManager gameManager;
  public GameObject defaultBadge;
  public string badgeName;
  public string badgeDescription;
  public bool achieved;
  public string condition;

  void Start() {
    achieved = false;
    this.gameObject.SetActive(false);
  }

  void Update() {
    if (badgeName == "Win all maps" 
    // && PlayerPrefs.GetInt(gameManager.Instance.gameStageStr) == condition
    ) {
      achieved = true;
      this.gameObject.SetActive(true);
      defaultBadge.SetActive(false);
    } else if (badgeName == "Have 20 diamonds" 
    // && gameManager.diamond == condition
    ) {
      achieved = true;
      this.gameObject.SetActive(true);
      defaultBadge.SetActive(false);
    } else if (badgeName == "Kill 100 enemies" 
    // && gameManager.killedEnemies == condition
    ) {
      achieved = true;
      this.gameObject.SetActive(true);
      defaultBadge.SetActive(false);
    }
  }
}