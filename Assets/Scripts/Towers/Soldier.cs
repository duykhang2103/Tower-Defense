using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Soldier : MonoBehaviour
{
    private Tower tower;
    protected Range range; 
    private Slider healthBar;
    protected Animator animator;
    public Enemy enemy;
    public int health = 150;
    public int maxHealth = 150;
    public int atk = 25;

    protected Vector3 lastPosition;
    public bool isMoving;

    public virtual void Start()
    {
        range = transform.Find("Range").gameObject.GetComponent<Range>(); 
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<Slider>();

        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }
        isMoving = false;
    }

    public virtual void Update()
    {
        if (enemy == null)
        {
            animator.SetBool("fight", false);
            MoveSoldier(lastPosition);
        }
        if (tower && tower.isFocused && !PointerOnUI() && Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = tower.transform.position.z; 


            
            Debug.Log("soldierPos" + transform.position);
            if (isMoveInRangeTower(mousePosition))
            {
                {
                    Debug.Log("MOVE");
                    lastPosition = mousePosition;
                    LetMoveSoldier(lastPosition);
                }
            }
        }
        
       
    }

    protected void LetMoveSoldier(Vector3 targetPosition)
    {
        lastPosition = targetPosition;
        isMoving = true;
    }

    private void MoveSoldier(Vector3 targetPosition)
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5.0f * Time.deltaTime);
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
            isMoving = false; 
        }
    }

    public void FindEnemyInRange() {

        if (enemy != null && !range.IsContainEnemy(enemy)) enemy = null;
        if (enemy == null && range.Get1Enemy() != null) {
            enemy = range.Get1Enemy();
            Attack();
        }
    }

    private bool isMoveInRangeTower(Vector3 position)
    {
        Vector3 center = tower.ScopeArea.transform.localPosition;
        CircleCollider2D range = tower.ScopeArea.GetComponent<CircleCollider2D>();
        Vector3 localPosition = tower.transform.InverseTransformPoint(position);
        
        float localRadius = range.radius * tower.ScopeArea.transform.localScale.x;
        return Vector3.Distance(localPosition, center) <= localRadius;
    }


    public void SetTower(Tower _tower)
    {
        tower = _tower;
    }

    virtual public void Attack()
    {
        // Debug.Log("Soldier attacks");
    }

    virtual public void TakeDamage(int damage)
    {
        // Debug.Log("soldier takes damage from enemy");
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
            else
            {
                healthBar.gameObject.SetActive(true);
            }
        }
    }
    private bool PointerOnUI()
    {
        bool isOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        return isOverUI;
    }
}
