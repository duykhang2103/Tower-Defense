using UnityEngine;

public class Badge : MonoBehaviour {
  public GameObject defaultBadge;
  public string badgeName;
  public string badgeDescription;
  public bool achieved;
  public string condition;

  // void Start() {
  //   achieved = false;
  //   this.gameObject.SetActive(false);
  // }

// TODO: Implement kill 100 enemies badge
  void Start() {
    Debug.Log("Diamonds: ");
    Debug.Log(PlayerPrefs.GetInt("Diamonds"));
    achieved = false;
    this.gameObject.SetActive(false);
    if ( badgeName == "Win all maps" 
    && PlayerPrefs.GetInt("Stage_1") > 0 && PlayerPrefs.GetInt("Stage_2") > 0
    ) {
      achieved = true;
      this.gameObject.SetActive(true);
      defaultBadge.SetActive(false);
    } else if (badgeName == "Earn 20 diamonds"  && PlayerPrefs.GetInt("Diamonds") >= 20
    ) {
      achieved = true;
      this.gameObject.SetActive(true);
      defaultBadge.SetActive(false);
    } else if (badgeName == "Kill total 20 enemies" && PlayerPrefs.GetInt("Enemies") >= 10

    // && gameManager.killedEnemies == condition
    ) {
      achieved = true;
      this.gameObject.SetActive(true);
      defaultBadge.SetActive(false);
    }
  }
}