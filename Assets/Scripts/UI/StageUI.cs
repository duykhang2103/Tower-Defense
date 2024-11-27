using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class StageUI : MonoBehaviour
{
    public string stageStr = "Stage_1";
    public GameObject starPref;
    public GameObject blackStarPref;
    public void Start() {
        Debug.Log("start stage");
        updateStage();
    }
    public void updateStage()
    {
        GameObject stage = GameObject.Find(stageStr);
        if (stage != null)
        {
            Debug.Log("found stage");
            for (int i = 1; i <= 3; i++) {
                Transform star = stage.transform.Find("star_" + i);
                if (star != null)
                {
                    Debug.Log("found star" + i);
                    ReplaceStar(star.gameObject, i);
                }
                else {
                    Debug.Log("not found star" + i);
                }
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy Stage: " + stageStr);
        }
    }
    private void ReplaceStar(GameObject reStar, int i)
    {
        Vector3 position = reStar.transform.position;
        Quaternion rotation = reStar.transform.rotation;
        Transform parent = reStar.transform.parent;

        Destroy(reStar);
        Debug.Log(stageStr + PlayerPrefs.GetInt(stageStr));
        if (PlayerPrefs.GetInt(stageStr) >= (i - 1 ) * 5 + 1) 
        Instantiate(starPref, position, rotation, parent); // yellow star
        else Instantiate(blackStarPref, position, rotation, parent); // blackstar
    }
}
