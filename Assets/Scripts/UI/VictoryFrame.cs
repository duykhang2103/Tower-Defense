using UnityEngine;

public class VictoryFrame : MonoBehaviour
{
    private void OnEnable()
    {
        UpdateVictoryFrame();
    }

    public void UpdateVictoryFrame()
    {
        int health = GameManager.Instance.playerHealth;
        for (int i = 1; i <= 3; i++)
        {
            GameObject star = FindObject("Star_" + i);
            UpdateStar(star, health, i);
        }
    }

    private GameObject FindObject(string starName)
    {
        return transform.Find(starName)?.gameObject;
    }
    private void UpdateStar(GameObject star, int health, int starIndex)
    {
        if (star != null)
        {
            if ((starIndex - 1) * 5 <= health)
            {
                star.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                star.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }
}
