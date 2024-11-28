using System.Collections.Generic; 
using UnityEngine;

public class Range : MonoBehaviour
{
    private Soldier soldier;
    private List<Enemy> enemies = new List<Enemy>(); 
    void Start()
    {
        soldier = GetComponentInParent<Soldier>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("Enemy entered range of soldier");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                soldier.FindEnemyInRange();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("Enemy left range of soldier");
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
                soldier.FindEnemyInRange();
            }
        }
    }
    public bool IsContainEnemy(Enemy enemy) {
        return enemies.Contains(enemy);
    }
    public Enemy Get1Enemy()
    {
        enemies.RemoveAll(enemy => enemy == null);
        return enemies.Count > 0 ? enemies[0] : null;
    }
}
