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
    private bool isFighting;
    private int currentPointIndex = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        LoadPath();
        SetSpawnPoint();
        StartMoving();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            EnterFightMode();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ExitFightMode();
        }
    }

    private void EnterFightMode()
    {
        animator.SetBool("isFighting", true); 
        isFighting = true;

        if (followPathCoroutine != null)
        {
            StopCoroutine(followPathCoroutine);
            followPathCoroutine = null;
        }
    }

    private void ExitFightMode()
    {
        animator.SetBool("isFighting", false); 
        isFighting = false;
        
        StartMoving();
    }

    private void StartMoving()
    {
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
