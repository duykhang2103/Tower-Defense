using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public WaveManager waveManager;
    public GameObject startWaveBtn;
    private void Start()
    {
        startWaveBtn.SetActive(true); 
    }

    public void OnStartWaveButtonClicked()
    {
        waveManager.StartWave();
        startWaveBtn.SetActive(false); 
        StartCoroutine(WaitForWaveEnd());
    }

    private IEnumerator WaitForWaveEnd()
    {
        yield return new WaitUntil(() => waveManager.IsWaveFinished);
        if (waveManager.noMoreWaves) yield break;
        startWaveBtn.SetActive(true); 
    }

    static public void OnGameOver()
    {
        Debug.Log("Game Over");
    }
}
