using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Soldier {
    public GameObject arrowPrefab;
    private float reloadTime = 1.0f;
    private float lastAttackTime = 0.0f;
    public override void Start() {
        base.Start();
        GameManager.ModifyGold(-15);
    }

    public override void Update() {
        base.Update();

        if (enemy != null && Time.time >= lastAttackTime + reloadTime) {
            shoot();
        }
    }

    public void shoot() {
        Debug.Log("Archer attacks");
        animator.SetBool("fight", true);


        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        Debug.Log("Archer created an Arrow");

        if (arrowScript != null) {
            arrowScript.Initialize(enemy.transform.position, enemy);
        }

       
        lastAttackTime = Time.time;

        if (enemy == null || enemy.health <= 0) {
            
            animator.SetBool("fight", false);
            FindEnemyInRange();
        }
    }
}
