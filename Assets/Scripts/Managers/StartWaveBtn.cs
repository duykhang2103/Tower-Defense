using UnityEngine;

public class StartWaveButton : MonoBehaviour
{
    public GameManager gameManager; 

    private void OnMouseDown()
    {
        Debug.Log("Start Wave Button Clicked!");
        gameManager.OnStartWaveButtonClicked();
    }
}
