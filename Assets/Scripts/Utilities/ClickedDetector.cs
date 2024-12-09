using UnityEngine;

public class ClickedDetector : MonoBehaviour
{
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0)) // 0 là nút chuột trái
        {
            Debug.Log("haved clicked");
            // DetectClickedObject();
            GetClickedObjectName();
        }
    }

    private void DetectClickedObject()
    {
       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit))
        {
           
            Debug.Log("Clicked on: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("No object hit");
        }
    }

    public string GetClickedObjectName()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider !=null) {
             return hit.collider.gameObject.name;
        }
        else
        {
            return "a";
        }
    }
}
