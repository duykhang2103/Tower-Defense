using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject soldier1 = null; 
    public GameObject soldier2 = null; 
    public GameObject soldier3 = null; 

    // Các nút để mua lính
    public GameObject ScopeArea;
    public GameObject buySoldierBtn1;
    public GameObject buySoldierBtn2;
    public GameObject buySoldierBtn3;

    void Start()
    {
        ScopeArea.SetActive(false);
        buySoldierBtn1.SetActive(false);
        buySoldierBtn2.SetActive(false);
        buySoldierBtn3.SetActive(false);
    }

    void Update()
    {
     
    }

    private void OnMouseDown()
    {
        Debug.Log("the tower is clicked");
        ScopeArea.SetActive(true);
        buySoldierBtn1.SetActive(true);
        buySoldierBtn2.SetActive(true);
        buySoldierBtn3.SetActive(true);
    }
}
