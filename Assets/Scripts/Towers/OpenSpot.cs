using UnityEngine;

public class OpenSpot : MonoBehaviour {
    public GameObject summoningTowerPrefab;
    public GameObject shootingTowerPrefab;
    private GameObject tower;
    private bool isActivated = false;
    public GameObject shootingTowerBtn = null;
    public GameObject summoningTowerBtn = null;
    private void OnMouseDown() {

        if (tower == null) {
            toggleActivate();
        }
    }

    private void toggleActivate() {
        isActivated = !isActivated;
        if (shootingTowerBtn) shootingTowerBtn.SetActive(isActivated);
        if (summoningTowerBtn) summoningTowerBtn.SetActive(isActivated);
    }

    public void BuildTower(string towerType) {
        Debug.Log("Building tower of type: " + towerType);
        if (tower == null) {
            if (towerType == "shooting") {
                tower = Instantiate(shootingTowerPrefab, transform.position, Quaternion.identity);
            } else if (towerType == "summoning") {
                tower = Instantiate(summoningTowerPrefab, transform.position, Quaternion.identity);
            }
            GameObject background = GameObject.Find("BackGround"); 
            if (background != null) {
                tower.transform.SetParent(background.transform);
            } else {
                Debug.LogWarning("Background not found!");
            }
            toggleActivate();
            this.gameObject.SetActive(false);
        }
    }

    private void Start() {
        if (shootingTowerBtn != null) shootingTowerBtn.SetActive(false);
        if (summoningTowerBtn != null) summoningTowerBtn.SetActive(false);
    }

}