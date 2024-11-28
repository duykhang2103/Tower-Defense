using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAniEvents : MonoBehaviour
{
    private Animator animator;
    private Enemy enemy;
    private Soldier soldier;
    void Start() {

        enemy = GetComponentInParent<Enemy>();
        animator = GetComponent<Animator>();
    }
    public void ApplyDameToSoldier() {
        soldier = enemy.soldier;
        if (soldier != null && soldier.health >= 0)
        {
            Debug.Log("ApplyDameToSoldier");
            soldier.TakeDamage(enemy.atk);
        }
        else {
            // Debug.Log("khono thay soldier hoajc soldier it mau hon 0");
            soldier = null;
            animator.SetBool("fight", false);
        }
    }
}
