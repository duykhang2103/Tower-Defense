using UnityEngine;

public class ClickedDetector : MonoBehaviour
{
    void Update()
    {
       
        if (Input.GetMouseButtonDown(0)) // 0 là nút chuột trái
        {
            Debug.Log("haved clicked");
            DetectClickedObject();
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
}
