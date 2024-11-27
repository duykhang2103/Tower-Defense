using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public string enemyName;
    [HideInInspector] public int maxHealth = 100;
    [HideInInspector] public int health = 100;
    [HideInInspector] public int atk = 10;
    [HideInInspector] public float speed = 2.0F;
    public int pathIndex = 1;
    public List<int> pathPointsIndices;
    public GameObject goldPrefab;
    private GameObject background;
    private Attribute attributes; 



    protected Animator animator;

    protected Soldier soldier;

    private Slider healthBar;
    private Coroutine followPathCoroutine;
    // private SpriteRenderer spriteRenderer;
    private int currentPointIndex = 0;
    private List<Transform> waypoints;



    public void Start()
    {
        attributes = GetComponent<Attribute>();
        if (attributes == null) {
            Debug.Log("attributes not found");
            return;
        }
        maxHealth = health = attributes.maxHealth;
        speed = attributes.speed;
        atk = attributes.atk;
        background = GameObject.Find("Background");
        if (!background)
        {
            Debug.LogError("Background object not found!");
            return;
        }
        animator = GetComponentInChildren<Animator>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<Slider>();
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }
       
        SetWaypoints();
        SetSpawnPoint();
        StartMoving();
    }
    void Update()
    {

    }
    public bool isTargeted()
    {
        return soldier != null;
    }

    public void Attack(Soldier _soldier)
    {
        // spriteRenderer.flipX = true;
        soldier = _soldier;
        Debug.Log("enemy attack to soldier");
        FightWithSoldier();
        if (followPathCoroutine != null)
        {
            StopCoroutine(followPathCoroutine);
        }

    }
    virtual public void FightWithSoldier()
    {
        //    animator.SetBool("fight", true);
    }
    virtual public void TakeDamage(int damage)
    {
        // Debug.Log("enemy take damage from soldier");
    }
    public void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
            if (healthBar.value == healthBar.maxValue)
            {
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

    
    private void SetSpawnPoint()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;
            Debug.Log("spawn point" + transform.position);
        }
        else
        {
            Debug.LogError("No waypoints available to set the spawn point.");
        }
    }
    private void SetWaypoints()
    {
        waypoints = new List<Transform>();
        GameObject waypointsParent = GameObject.Find($"WayPoints_{pathIndex}");
        if (waypointsParent == null)
        {
            Debug.LogError($"WayPoints_{pathIndex} not found in the scene!");
            return;
        }

        foreach (Transform child in waypointsParent.transform)
        {
            waypoints.Add(child);
            Debug.Log($"Waypoint: {child.name}, Position: {child.position}");
        }
        if (waypoints.Count == 0)
        {
            Debug.LogError($"No Waypoint_{pathIndex} children found!");
        }
    }

    private IEnumerator FollowPath()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints available.");
            yield break;
        }

        while (currentPointIndex < waypoints.Count)
        {
            Transform targetWaypoint = waypoints[currentPointIndex];
            Vector3 targetPosition = targetWaypoint.position;
            Debug.Log("position now" + transform.position);
            Debug.Log("target position" + targetPosition);
            while (Vector3.Distance(transform.position , targetPosition) > 0.1f)
            {
                Debug.Log("thoa dieu kien");
                if (soldier) yield break;

                transform.position = Vector3.MoveTowards(transform.position , targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            currentPointIndex++;
        }

        Destroy(this.gameObject);
        GameManager.ModifyPlayerHealth(-1);
    }
    private void OnDestroy()
    {
        Vector3 position = transform.position ;
        Destroy(gameObject);

        if (goldPrefab != null)
        {
            GameObject coin = Instantiate(goldPrefab, position, Quaternion.identity);
            Destroy(coin, 2f);
        }
        GameManager.ModifyGold(5);


    }
}
