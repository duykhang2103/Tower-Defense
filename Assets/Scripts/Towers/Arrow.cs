using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour {
    private Vector3 targetPosition;
    private Enemy targetEnemy;
    private float speed = 15f;
    private float arcHeight = 3f;

    public void Initialize(Vector3 target, Enemy enemy) {
        if (enemy == null) Destroy(gameObject); 
        targetPosition = target;
        targetEnemy = enemy;
        StartCoroutine(ArcMovement());
        
    }


    private IEnumerator ArcMovement() {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float time = 0f;

        while (time < 1f) {
            if (targetEnemy == null) Destroy(this); 
            time += Time.deltaTime * speed / distance;
            Vector3 currentPos = Vector3.Lerp(startPosition, targetPosition, time);
            float heightOffset = arcHeight * Mathf.Sin(Mathf.PI * time); // parabolic
            currentPos.y += heightOffset;

            transform.position = currentPos;

            yield return null;
        }

        Destroy(gameObject); 
        targetEnemy.TakeDamage(10);
    }
}
