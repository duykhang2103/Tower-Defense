using UnityEngine;

public class OpenSpot : MonoBehaviour {
    public GameObject summoningTowerPrefab;
    public GameObject shootingTowerPrefab;
    private GameObject tower;
    private bool isActivated = false;
    public GameObject shootingTowerBtn;
    public GameObject summoningTowerBtn;
    private void OnMouseDown() {

        if (tower == null) {
            toggleActivate();
        }
    }

    private void toggleActivate() {
        isActivated = !isActivated;
        shootingTowerBtn.SetActive(isActivated);
        summoningTowerBtn.SetActive(isActivated);
    }

    public void BuildTower(string towerType) {
        Debug.Log("Building tower of type: " + towerType);
        if (tower == null) {
            if (towerType == "shooting") {
                tower = Instantiate(shootingTowerPrefab, transform.position, Quaternion.identity);
            } else if (towerType == "summoning") {
                tower = Instantiate(summoningTowerPrefab, transform.position, Quaternion.identity);
            }
            toggleActivate();
            this.gameObject.SetActive(false);
        }
    }

    private void Start() {
        shootingTowerBtn.SetActive(false);
        summoningTowerBtn.SetActive(false);
    }

}