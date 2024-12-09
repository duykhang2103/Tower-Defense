using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<int> monsterIds; 
}

public class WaveManager : MonoBehaviour
{
    private MonsFactory monsFactory; 
    [SerializeField]
    private List<Wave> monsters;  
    public int[] pathIds;
    public float spawnInterval = 1f;
    public int numWaves = 5;
    private int currentWave = 0;
    private int enemiesSpawned = 0;
    private bool isWaveFinished = true; 
    public GameObject startWaveBtn;
    public bool IsWaveFinished => isWaveFinished;
    public bool noMoreWaves => (currentWave >= monsters.Count);  

    void Start()
    {
        monsFactory = GetComponent<MonsFactory>();
        if (monsFactory == null)
        {
            Debug.LogError("MonsFactory component not found");
        }
    }
    public void Update() {
        if (noMoreWaves || !isWaveFinished) startWaveBtn.SetActive(false);
        else {
            startWaveBtn.SetActive(true);
        }
    }

    public void StartWave()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        if (isWaveFinished) 
        {
            
            isWaveFinished = false;
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        enemiesSpawned = 0;
        Debug.Log($"Spawn interval: {spawnInterval} seconds");


        foreach (int monsterId in monsters[currentWave].monsterIds)
        {
            foreach(int pathId in pathIds)
            {
                GameObject enemy = monsFactory.Create(monsterId, transform.position, Quaternion.identity);
                enemiesSpawned++;
                enemy.GetComponent<Enemy>().SetPath(pathId);
            }
            yield return new WaitForSeconds(spawnInterval);
        }

        currentWave++;

        isWaveFinished = true; 
    }
}
