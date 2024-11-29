using System.Collections;
using System.Collections.Generic;
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
    public Soldier soldier;

    private Slider healthBar;
    private int currentPointIndex = 0;
    private List<Transform> waypoints;

    public void Start()
    {
        attributes = GetComponent<Attribute>();
        if (attributes == null)
        {
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
        healthBar = GetComponentInChildren<Slider>();
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }

        SetWaypoints();
        SetSpawnPoint();
    }

    public virtual void Update()
    {

        if (soldier != null)
        {
            Debug.Log("enemy fight");
            animator.SetBool("fight", true);
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        if (soldier == null || soldier.health <= 0)
        {
            animator.SetBool("run", true);
            animator.SetBool("fight", false);

            soldier = null;
            MoveAlongPath();
        }
    }
    public void Attack(Soldier _soldier)
    {
        Debug.Log("enemy attack ");
        soldier = _soldier;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Torch takes " + damage + " damage");
        health -= damage;
        UpdateHealthBar();
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    private void MoveAlongPath()
    {
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No waypoints available.");
            return;
        }

        if (currentPointIndex < waypoints.Count)
        {
            Transform targetWaypoint = waypoints[currentPointIndex];
            Vector3 targetPosition = targetWaypoint.position;
            if (transform.position.x < targetPosition.x) {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPointIndex++;
            }
        }
        else
        {
            Destroy(this.gameObject);
            GameManager.ModifyPlayerHealth(-1);
        }
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

    private void SetSpawnPoint()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;
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
        }

        if (waypoints.Count == 0)
        {
            Debug.LogError($"No Waypoints found for pathIndex {pathIndex}");
        }
    }

    private void OnDestroy()
    {
        Vector3 position = transform.position;

        if (soldier)
        {
            soldier.enemy = null;
            soldier.FindEnemyInRange();
        }

        Destroy(gameObject);

        if (goldPrefab != null)
        {
            GameObject coin = Instantiate(goldPrefab, position, Quaternion.identity);
            Destroy(coin, 2f);
        }

        GameManager.ModifyGold(5);
    }
}
