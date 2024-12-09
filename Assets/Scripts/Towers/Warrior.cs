using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Soldier {
    private Vector3 targetPosition;
    private bool isAttacking = false;

    public override void Start() {
        base.Start();
        GameManager.ModifyGold(-10);
        isMoving = false;
        isAttacking = false;
    }
    public override void Update() {
        if (enemy) {
            if (!isAttacking) MoveWarriorToPosition (targetPosition);
        }
        base.Update();
    }

    public override void Attack() {
        if (enemy != null && enemy.soldier == null) {
            isAttacking = false;
            // Debug.Log("warrior attack");
            Vector3 oppositePositionWithEnemy = enemy.transform.position + Vector3.left;
            targetPosition = oppositePositionWithEnemy;
            enemy.Attack(this);
        }
    }


    private void ApplyDamageToEnemy() {
        if (enemy != null) {
            AudioSource audioSlash = GetComponent<AudioSource>();
            audioSlash.Play();
            enemy.TakeDamage(atk);
        }
        else {
            animator.SetBool("fight", false);
            // Debug.Log("no enemy");
            FindEnemyInRange();
        }
    }

    public override void TakeDamage(int damage) {
        // Debug.Log("warrior take " + damage + " damage");
        health -= damage;
        UpdateHealthBar();
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    

    private void MoveWarriorToPosition(Vector3 targetPosition) {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 15.0f * Time.deltaTime);
            animator.SetBool("run", true);
        } else {
            animator.SetBool("run", false);
            animator.SetBool("fight", true);
            isAttacking = true;
        }
    }
}
