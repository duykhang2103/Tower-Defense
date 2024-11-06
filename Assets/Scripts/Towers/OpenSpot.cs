using UnityEngine;

public class OpenSpot : MonoBehaviour {
    public GameObject towerPrefab;
    private GameObject tower;

    private void OnMouseDown() {
        Debug.Log("Clicked on open spot");
        if (tower == null) {
            tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        }
    }
}