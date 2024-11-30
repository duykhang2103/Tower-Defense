using UnityEngine;

public class HealthManager : MonoBehaviour {
    // public GameManager gameManager;
    public int health = 100;
    public int maxHealth = 100;

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            GameManager.Defeat();

          }
    }


}