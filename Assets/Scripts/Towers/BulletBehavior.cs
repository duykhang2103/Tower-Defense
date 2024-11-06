using UnityEngine;

public class BulletBehavior : MonoBehaviour {

  public GameManager gameManager;
  private float speed = 10;
  private float damage = 1;
  public GameObject target;
  public Vector3 startPosition;
  public Vector3 targetPosition;
  private float distance;
  private float startTime;

  void Start() {
    startTime = Time.time;
    distance = Vector3.Distance(startPosition, targetPosition);
  }

  void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =
                    healthBarTransform.gameObject.GetComponent<HealthBar>();
                healthBar.currentHealth = Mathf.Max(healthBar.currentHealth - damage, 0);

                if (healthBar.currentHealth <= 0)
                {
                    Destroy(target);
                    // gameManager.Gold += 50;
                }
            }
            Destroy(gameObject);
        }
    }


}