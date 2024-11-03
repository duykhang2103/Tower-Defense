using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Soldier {
    public override void Attack(Enemy enemy) {
        if (enemy != null) 
        {
            Vector3 oppositePositionWithEnemy = enemy.transform.position + Vector3.left; 
            
            StartCoroutine(MoveWarriorToPosition(oppositePositionWithEnemy));
            FightWithEnemy(enemy);
            enemy.FightWithSoldier();
        }
      
    }
    private void FightWithEnemy(Enemy enemy) {
        animator.SetBool("fight", true);
    }
    private IEnumerator MoveWarriorToPosition(Vector3 targetPosition) 
    {
        animator.SetBool("run", true); 
        Debug.Log("da set animator thanh true");
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 15.0f * Time.deltaTime); 
            yield return null; 
        }
        animator.SetBool("run", false);
    }
}
