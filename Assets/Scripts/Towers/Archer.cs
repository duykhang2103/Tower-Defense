using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Soldier {
    public GameObject arrowPrefab;
    private float reloadTime = 1.0f;

    public override void Start() {
        base.Start();
        GameManager.ModifyGold(-15);
    }
    public override void Attack() {
        Debug.Log("Archer attacks");
        StartCoroutine(ContinuousAttack());
        
    }
    private IEnumerator ContinuousAttack() {
        while (enemy != null && enemy.health >= 0) { 
            Debug.Log("Archer attacks");
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            Arrow arrowScript = arrow.GetComponent<Arrow>();
            Debug.Log("Archer created an Arrow");
            if (arrowScript != null) {
                arrowScript.Initialize(enemy.transform.position, enemy); 
            }
            
            yield return new WaitForSeconds(reloadTime);
        }
    }
    
}
