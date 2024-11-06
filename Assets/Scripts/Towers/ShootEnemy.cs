using UnityEngine;
using System.Collections.Generic;

public class ShootEnemy : MonoBehaviour {
  public List<GameObject> enemiesInRange;
  private float lastShotTime;
  private ShootingTower tower;
  void Shoot(Collider2D target)
{
  GameObject bulletPrefab = tower.bullet;
  // 1 
  Vector3 startPosition = gameObject.transform.position;
  Vector3 targetPosition = target.transform.position;
  startPosition.z = bulletPrefab.transform.position.z;
  targetPosition.z = bulletPrefab.transform.position.z;

  // 2 
  GameObject newBullet = (GameObject)Instantiate (bulletPrefab);
  newBullet.transform.position = startPosition;
  BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
  bulletComp.target = target.gameObject;
  bulletComp.startPosition = startPosition;
  bulletComp.targetPosition = targetPosition;

}



  private void Start() {
    enemiesInRange = new List<GameObject>();  
    lastShotTime = Time.time;
  }

  void OnEnemyDestroy(GameObject enemy)
  {
    enemiesInRange.Remove (enemy);
  }

  void OnTriggerEnter2D (Collider2D range)
  {
    if (range.gameObject.tag.Equals("Enemy"))
    {
      enemiesInRange.Add(range.gameObject);
      EnemyDestructionDelegate del =
          range.gameObject.GetComponent<EnemyDestructionDelegate>();
      del.enemyDelegate += OnEnemyDestroy;
    }
  }

  void OnTriggerExit2D (Collider2D range)
  {
    if (range.gameObject.tag.Equals("Enemy"))
    {
      enemiesInRange.Remove(range.gameObject);
      EnemyDestructionDelegate del =
          range.gameObject.GetComponent<EnemyDestructionDelegate>();
      del.enemyDelegate -= OnEnemyDestroy;
    }
  }

}