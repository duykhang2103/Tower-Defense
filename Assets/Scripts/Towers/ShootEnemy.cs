using UnityEngine;
using System.Collections.Generic;

public class ShootEnemy : MonoBehaviour {
  public List<GameObject> enemiesInRange;
  private float lastShotTime;
  private ShootingTower tower;
  void Shoot(Collider2D target)
{
  GameObject bulletPrefab = tower.bullet;
  Vector3 startPosition = gameObject.transform.position;
  Vector3 targetPosition = target.transform.position;
  startPosition.z = bulletPrefab.transform.position.z;
  targetPosition.z = bulletPrefab.transform.position.z;

  GameObject newBullet = Instantiate (bulletPrefab);
  newBullet.transform.position = startPosition;
  BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
  bulletComp.target = target.gameObject.GetComponent<Enemy>();
  bulletComp.startPosition = startPosition;
  bulletComp.targetPosition = targetPosition;

}

  private void Start() {
    enemiesInRange = new List<GameObject>();  
    lastShotTime = Time.time;
    tower = gameObject.GetComponentInChildren<ShootingTower>();
  }

  private void Update() {
    GameObject target = null;
    float minimalEnemyDistance = float.MaxValue;
    foreach (GameObject enemy in enemiesInRange) {
      float distanceToGoal = Vector2.Distance(enemy.transform.position, gameObject.transform.position);
      if (distanceToGoal < minimalEnemyDistance) {
        target = enemy;
        minimalEnemyDistance = distanceToGoal;
      }
    }
    if (target != null) {
      if (Time.time - lastShotTime > tower.fireRate) {
        Shoot(target.GetComponent<Collider2D>());
        lastShotTime = Time.time;
      }
      Vector3 direction = gameObject.transform.position - target.transform.position;
      gameObject.transform.rotation = Quaternion.AngleAxis(
          Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
          new Vector3(0, 0, 1));
    }
    
  }

  void OnEnemyDestroy(GameObject enemy)
  {
    enemiesInRange.Remove (enemy);
  }

  void OnTriggerEnter2D (Collider2D range)
  {
    if (range.gameObject.tag.Equals("enemy"))
    {
      enemiesInRange.Add(range.gameObject);
      // EnemyDestructionDelegate del =
      //     range.gameObject.GetComponent<EnemyDestructionDelegate>();
      // del.enemyDelegate += OnEnemyDestroy;
    }
  }

  void OnTriggerExit2D (Collider2D range)
  {
    if (range.gameObject.tag.Equals("enemy"))
    {
      enemiesInRange.Remove(range.gameObject);
      // EnemyDestructionDelegate del =
      //     range.gameObject.GetComponent<EnemyDestructionDelegate>();
      // del.enemyDelegate -= OnEnemyDestroy;
    }
  }

}