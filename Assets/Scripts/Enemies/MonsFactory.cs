using System.Collections.Generic;
using UnityEngine;

public class MonsFactory : MonoBehaviour
{
   

    [SerializeField]
    private List<GameObject> monsterList = new List<GameObject>(); 

    public GameObject Create(int id, Vector3 position, Quaternion rotation)
    {
        if (id > 0 && id <= monsterList.Count)
        {
            GameObject prefab = monsterList[id - 1];
            return Instantiate(prefab, position, rotation);
        }

        Debug.LogWarning($"Invalid monster ID: {id}");
        return null;
    }
}
