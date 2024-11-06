using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Soldier : MonoBehaviour
{

    private bool isFocused;
    private Tower tower;
    private Coroutine moveCoroutine;
    private CircleCollider2D detectionRange;

    private Slider healthBar;
    protected Animator animator;
    protected Enemy enemy;
    public int health = 150;
    public int maxHealth = 150;
    public int atk = 25;

    protected Vector3 lastPosition;


    public virtual void Start()
    {
        moveCoroutine = null;
        isFocused = false;
        detectionRange = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<Slider>();
        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            UpdateHealthBar();
        }
    }
    void Update()
    {

        if (!enemy && Input.GetMouseButtonDown(0) && isFocused) // move soldier
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseMousePos = tower.transform.InverseTransformPoint(mousePos);
            mouseMousePos.z = -1;
            if (isMoveInRangeTower(mouseMousePos))
            {
                lastPosition = mouseMousePos;
                LetMoveSoldier(lastPosition);
            }
        }
    }
    protected void LetMoveSoldier(Vector3 mouseMousePos)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MoveSoldier(mouseMousePos));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if enemy in range 
        if (enemy == null && other.tag == "enemy")
        {
            enemy = other.GetComponent<Enemy>();
            if (enemy.isTargeted()) return;
            Attack();
            if (this is Warrior)
            {
                enemy.Attack(this);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // if enemy is out of range
        if (enemy != null && other.gameObject == enemy.gameObject)
        {
            Debug.Log("enemy is out of range1");
            enemy = null;
        }
    }
    private bool isMoveInRangeTower(Vector3 position)
    {
        Vector3 center = tower.ScopeArea.transform.localPosition;
        CircleCollider2D range = tower.ScopeArea.GetComponent<CircleCollider2D>();

        float localRadius = range.radius * tower.ScopeArea.transform.localScale.x;

        return Vector3.Distance(position, center) <= localRadius;
    }
    private IEnumerator MoveSoldier(Vector3 targetPosition)
    {

        animator.SetBool("run", true);
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.1f)
        {
            if (enemy) yield break;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, 5.0f * Time.deltaTime);
            yield return null;
        }
        animator.SetBool("run", false);
    }
    private void OnMouseDown()
    {
        isFocused = !isFocused;
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
        // Debug.og("soldier take damage from enemy");
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

}
