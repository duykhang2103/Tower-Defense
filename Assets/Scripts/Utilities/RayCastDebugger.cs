using UnityEngine;

public class RaycastDebugger2D : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
            if (hits.Length > 0)
            {
                foreach (RaycastHit2D hit in hits)
                {
                    Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
                }
            }
            else
            {
                Debug.Log("No objects hit.");
            }
        }
    }
}