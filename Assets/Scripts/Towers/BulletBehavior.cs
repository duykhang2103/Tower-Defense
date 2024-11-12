using UnityEngine;

public class BulletBehavior : MonoBehaviour {

  public GameManager gameManager;
  private float speed = 10;
  private float damage = 1;
  public Enemy target = null;
  public Vector3 startPosition;
  public Vector3 targetPosition;
  private float distance;
  private float startTime;

  void Start() {
    startTime = Time.time;
    distance = Vector3.Distance(startPosition, targetPosition);
    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
  }

  void Update()
    {
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        if (gameObject.transform.position.Equals(targetPosition))
        {
            if (target != null)
            {
                target.TakeDamage(10);

                if (target.health <= 0)
                {
                    Destroy(target);
                }
            }
            Destroy(gameObject);
        }
    }


}