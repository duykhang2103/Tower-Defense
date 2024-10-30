using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class PathData
{
    public List<Vector3> points;
}

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> points = new List<Vector3>(); 
    private string filePath;
    public string filePathName;

    private void Start()
    {
     
        filePath = Application.dataPath + "/Data/Paths/" + filePathName +".json";

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0; 
        lineRenderer.startWidth = 0.1f; 
        lineRenderer.endWidth = 0.1f; 
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); 
        lineRenderer.startColor = Color.green; 
        lineRenderer.endColor = Color.green; 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; 
            AddPoint(mousePos);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PrintPoints();
            SavePointsToFile();
        }
    }

    private void AddPoint(Vector3 point)
    {
        points.Add(point); 
        lineRenderer.positionCount = points.Count; 
        lineRenderer.SetPosition(points.Count - 1, point); 
    }

    private void PrintPoints()
    {
        Debug.Log("Tọa độ các điểm vẽ:");
        foreach (var point in points)
        {
            Debug.Log(point);
        }
    }

    private void SavePointsToFile()
    {
       
        string directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        
        PathData pathData = new PathData { points = points };
        string json = JsonUtility.ToJson(pathData, true); 
        File.WriteAllText(filePath, json);
        
        Debug.Log("Dữ liệu đã được lưu vào file JSON: " + filePath);
    }
}
