using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Enemy
{

    public override void FightWithSoldier()
    {
  
        if (soldier != null)
        {
            animator.SetBool("fight", true);
            Debug.Log($"{enemyName} (Torch) is fighting the soldier!");
            StartCoroutine(ApplyDamageToSoldier());
        }
    }

    private IEnumerator ApplyDamageToSoldier()
    {
        Debug.Log("ApllyDamageToSoldier run");
        while (soldier != null && health > 0)
        {
      
            yield return new WaitForSeconds(1f);

            soldier.TakeDamage(atk);
            Debug.Log($"{enemyName} attacks soldier for {atk} damage!");

            if (soldier.health <= 0)
            {
                Debug.Log("Soldier has been defeated.");
                soldier = null;
                animator.SetBool("fight", false);
                StartMoving();
                
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("torch take " + damage + " damage");
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}