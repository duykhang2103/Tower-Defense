using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public string enemyName;        
    public int maxHealth = 100; 
    public int health = 100;
    public int atk = 10;
    public float speed;               
    public string pathFileName;          
    public List<Vector3> pathPoints;

    protected Animator animator;

    protected Soldier soldier;

    private Slider healthBar;
    private Coroutine followPathCoroutine;
    private SpriteRenderer spriteRenderer;
    private int currentPointIndex = 0;
    

    
    public void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<Slider>();
        if (healthBar) {
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }
        

        LoadPath();
        SetSpawnPoint();
        StartMoving();
    }
    void Update()
    {
        
    }
    public bool isTargeted() {
        return soldier != null;
    }

    public void Attack(Soldier _soldier) {
        spriteRenderer.flipX = true;
        soldier = _soldier;
        Debug.Log("enemy attack to soldier");
        FightWithSoldier();
        if (followPathCoroutine != null) {
            StopCoroutine(followPathCoroutine);
        }
        
    }
    virtual public void FightWithSoldier() {
    //    animator.SetBool("fight", true);
    }
    virtual public void TakeDamage(int damage) {
        // Debug.Log("enemy take damage from soldier");
    }
    public void UpdateHealthBar() {
        if (healthBar != null)
        {
            healthBar.value = health; 
            if (healthBar.value == healthBar.maxValue) {
                healthBar.gameObject.SetActive(false);
            }
            else healthBar.gameObject.SetActive(true);
        }
       
    }
    protected void StartMoving()
    {
        animator.SetBool("run", true);
        followPathCoroutine = StartCoroutine(FollowPath());
    }

    private void LoadPath()
    {
        string fullPath = Path.Combine(Application.dataPath, "Data/Paths", pathFileName);
        
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            PathData pathData = JsonUtility.FromJson<PathData>(json);
            pathPoints = pathData.points;
        }
        else
        {
            Debug.LogError($"Path file not found for {enemyName}: {fullPath}");
        }
    }

    private void SetSpawnPoint()
    {
        if (pathPoints != null && pathPoints.Count > 0)
        {
            transform.position = pathPoints[0]; // Set spawn point to the first path point
        }
        else
        {
            Debug.LogError("No path points available to set the spawn point.");
        }
    }

    private IEnumerator FollowPath()
    {
        if (pathPoints == null || pathPoints.Count == 0)
        {
            Debug.LogError("No path points available.");
            yield break;
        }

        while (currentPointIndex < pathPoints.Count)
        {
            Vector3 targetPosition = pathPoints[currentPointIndex];
            if (targetPosition.x < transform.position.x) {
                 spriteRenderer.flipX = false;
            } else {
                spriteRenderer.flipX = true;
            }
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                if (soldier) yield break;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            currentPointIndex++;
        }

        Destroy(this.gameObject);
        GameManager.ModifyPlayerHealth(-1);
    }
}
