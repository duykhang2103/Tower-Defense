using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySoldierBtn : MonoBehaviour
{
    public Tower tower; 
    public string soldierType; 

    private void OnMouseDown() {
        if (tower != null) {
            StartCoroutine(tower.BuySoldier(soldierType));
        }
    }
}
