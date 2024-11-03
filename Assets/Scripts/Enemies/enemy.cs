using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;         
    public float speed;               
    public string pathFileName;          
    public List<Vector3> pathPoints;

    private Animator animator;
    private Coroutine followPathCoroutine;
    private SpriteRenderer spriteRenderer;
    private bool isFighting;
    private int currentPointIndex = 0;
    private Soldier soldier;
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (followPathCoroutine != null) {
            StopCoroutine(followPathCoroutine);
        }
            
        
    }
    public void FightWithSoldier() {
       animator.SetBool("fight", true);
    }
    private void StartMoving()
    {
        animator.SetBool("run", true);
        if (!isFighting && followPathCoroutine == null)
        {
            followPathCoroutine = StartCoroutine(FollowPath());
        }
    }

    private void LoadPath()
    {
        string fullPath = Path.Combine(Application.dataPath, "Data/Paths", pathFileName);
        Debug.Log($"Path is {fullPath}");
        
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            PathData pathData = JsonUtility.FromJson<PathData>(json);
            pathPoints = pathData.points;
            
            Debug.Log($"Loaded path for {enemyName}: {pathPoints.Count} points.");
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
                 spriteRenderer.flipX = true;
            } else {
                spriteRenderer.flipX = false;
            }
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                if (isFighting) yield break;

                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            currentPointIndex++;
        }

        Destroy(this.gameObject);
    }
}
