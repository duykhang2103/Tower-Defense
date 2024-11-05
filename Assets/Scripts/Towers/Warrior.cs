using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Soldier {
    protected override void Start() {
        base.Start();
        GameManager.ModifyGold(-10);
    }
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
        StartCoroutine(ApplyDamageToEnemy(enemy));
    }
    private IEnumerator ApplyDamageToEnemy(Enemy enemy)
    {
        Debug.Log("ApllyDamageToEnemy run");
        while (enemy != null && health > 0)
        {
            yield return new WaitForSeconds(1f); 
            enemy.TakeDamage(atk);
            Debug.Log($"soldier attacks enemy for {atk} damage!");
            if (enemy.health <= 0)
            {
                animator.SetBool("fight", false);
                yield break;
            }
        }
    }
    private IEnumerator MoveWarriorToPosition(Vector3 targetPosition) 
    {
        animator.SetBool("run", true); 
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 15.0f * Time.deltaTime); 
            yield return null; 
        }
        animator.SetBool("run", false);
    }
    public override void TakeDamage(int damage)
    {
        Debug.Log("warrior take " + damage + " damage");
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
