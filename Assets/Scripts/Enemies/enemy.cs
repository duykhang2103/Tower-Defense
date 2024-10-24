using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;         
    public float speed;               
    public string pathFileName;       
    public Animator animator;          
    public List<Point> pathPoints;    

    void Start()
    {
        if (GetComponent<SpriteRenderer>() == null)
        {
            gameObject.AddComponent<SpriteRenderer>();
        }
        LoadPath();
        StartCoroutine(FollowPath());
    }

    private void LoadPath()
    {
        string fullPath = Path.Combine(Application.dataPath, "Data/Paths", pathFileName);
        Debug.Log($"path is {fullPath}");
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

    private IEnumerator FollowPath()
    {
        if (pathPoints == null || pathPoints.Count == 0)
        {
            Debug.LogError("No path points available.");
            yield break;
        }

        foreach (Point point in pathPoints)
        {
            Vector3 targetPosition = new Vector3(point.x, point.y, point.z);
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                // Di chuyển kẻ thù
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

                // Bắt đầu animation (nếu có)
                if (animator != null)
                {
                    animator.SetBool("IsMoving", true); // Đảm bảo có boolean trong Animator
                }

                yield return null; // Tạm dừng cho đến frame tiếp theo
            }

            // Dừng animation khi đến đích
            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }
}

[System.Serializable]
public class Point
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class PathData
{
    public List<Point> points;
}
