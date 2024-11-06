using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Soldier {
    public override void Start() {
        base.Start();
        GameManager.ModifyGold(-10);
    }
    public override void Attack() {
        if (enemy != null) 
        {
            Vector3 oppositePositionWithEnemy = enemy.transform.position + Vector3.left; 
            StartCoroutine(MoveWarriorToPosition(oppositePositionWithEnemy));
            FightWithEnemy();
            enemy.FightWithSoldier();
        }
      
    }
    private void FightWithEnemy() {
        animator.SetBool("fight", true);
        StartCoroutine(ApplyDamageToEnemy());
    }
    private IEnumerator ApplyDamageToEnemy()
    {
        Debug.Log("ApllyDamageToEnemy run");
        while (enemy != null && health > 0)
        {
            
            yield return new WaitForSeconds(1f); 
            if (enemy != null) {
                enemy.TakeDamage(atk);
                Debug.Log($"soldier attacks enemy for {atk} damage!");
                if (enemy && enemy.health <= 0)
                {
                    animator.SetBool("fight", false);
                    
                    enemy = null;
                    LetMoveSoldier(lastPosition);
                    yield break;
                }
                else if (!enemy) {
                    animator.SetBool("fight", false);
                    LetMoveSoldier(lastPosition);
                }
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
