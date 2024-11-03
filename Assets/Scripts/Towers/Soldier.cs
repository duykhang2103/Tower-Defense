using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private float health;
    private float attackPower;
    private float range;

    private bool isFocused;
    private Tower tower;
    private Coroutine moveCoroutine;
    private CircleCollider2D detectionRange; 
    protected Animator animator; 
    private Enemy enemy;
    
    void Start()
    {
        moveCoroutine = null;
        isFocused = false;
        detectionRange = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && isFocused) // move soldier
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseMousePos = tower.transform.InverseTransformPoint(mousePos);
            mouseMousePos.z = -1;
            if (isMoveInRangeTower(mouseMousePos))
            {
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
                
                moveCoroutine = StartCoroutine(MoveSoldier(mouseMousePos));
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (enemy == null &&  other.tag == "enemy") {
            enemy = other.GetComponent<Enemy>();
            if (enemy.isTargeted()) return;
            Attack(enemy);
            enemy.Attack(this);
        }
    }
    private bool isMoveInRangeTower(Vector3 position) {
        Vector3 center = tower.ScopeArea.transform.localPosition;
        CircleCollider2D range = tower.ScopeArea.GetComponent<CircleCollider2D>();

        float localRadius = range.radius * tower.ScopeArea.transform.localScale.x;
        
        return Vector3.Distance(position, center) <= localRadius;
    }
    private IEnumerator MoveSoldier(Vector3 targetPosition) {
        
        animator.SetBool("run", true); 
        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.1f) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, 5.0f * Time.deltaTime);
            yield return null;
        }
        animator.SetBool("run", false); 
    }
    public virtual void Attack(Enemy enemy)
    {
        Debug.Log("Soldier attacks");
    }
    private void OnMouseDown()
    {
        Debug.Log("isFocused");
        isFocused = !isFocused;
    }
    public void SetTower(Tower _tower) {
        tower = _tower;
    }

}
