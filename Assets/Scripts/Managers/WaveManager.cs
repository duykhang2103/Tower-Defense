using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;
    public int waveSize = 5;
    public int numWaves = 5;
    private int currentWave = 1;
    private int enemiesSpawned = 0;
    private bool isWaveFinished = true; 

    public bool IsWaveFinished => isWaveFinished;
    public bool noMoreWaves => (currentWave > numWaves);
    public void StartWave()
    {
        if (isWaveFinished) // Start wave only if the previous wave is finished
        {
            isWaveFinished = false;
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        enemiesSpawned = 0;
        Debug.Log($"Spawn interval: {spawnInterval} seconds"); 
        for (int i = 0; i < waveSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();    

            // Set enemy name based on wave and spawn number
            enemyScript.enemyName = $"Enemy {i + 1} in Wave {currentWave}";

            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }

        currentWave++;

        isWaveFinished = true; 
    }
}
