using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    Transform[] spawnPoints;
    [SerializeField]
    float spawnWait = 1f;
    [SerializeField]
    Wave[] waves;
    
    [SerializeField]
    GameObject zombie;
    [SerializeField]
    GameObject[] player;
    public int ongoingSpawnProcesses;
    public bool start, stop;
    [SerializeField]
    Zombie small, giant;
    bool coroutineHasStarted;
    [SerializeField]
    Shop shop;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        SpawnPlayer();
        
        AddToPlayerEvent();
        shop = GameObject.Find("Shop").GetComponent<Shop>();
        int i = FindObjectsOfType<SpawnManager>().Length;
        if (i > 1) Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Player.PlayerHasDied -= StopSpawning;
        Player.PlayerHasSpawned -= StartSpawning;
    }
    #endregion

    #region Public Methods
    public void AddToPlayerEvent()
    {
        Player.PlayerHasDied += StopSpawning;
        Player.PlayerHasSpawned += StartSpawning;
        start = true;
        stop = true;
    }

    public void StartSpawning()
    {
        Time.timeScale = 1;
        ongoingSpawnProcesses++;
        if (!coroutineHasStarted)
        {
            StartCoroutine(SpawnZombies());
        }
    }

    public void StopSpawning()
    {
        //StopAllCoroutines();
        coroutineHasStarted = false;
        Time.timeScale = 0;
        ongoingSpawnProcesses = 0;
    }

    public void SpawnPlayer()
    {
        shop.LoadStats();
        if (shop.weaponSelection == 0)
            Instantiate(player[0], new Vector2(0f, -3f), Quaternion.identity);
        else
            Instantiate(player[1], new Vector2(0f, -3f), Quaternion.identity);
    }

    public IEnumerator SpawnZombies()
    {
        coroutineHasStarted = true;
        while (true)
        {
            float i = Random.Range(0f, 1f);
            if (i > 0.35f)
            {
                int spawn = Random.Range(0, spawnPoints.Length);
                Instantiate(small, spawnPoints[spawn].position, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(spawnWait - 3, spawnWait + 1));
            }
            else
            {
                int spawn = Random.Range(0, spawnPoints.Length);
                Instantiate(giant, spawnPoints[spawn].position, Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(spawnWait - 3, spawnWait + 1));
            }
        }
        /*foreach (Wave wave in waves)
        {
            waveCount++;
            while (smallZombieCount < wave.regularAmount && giantZombieCount < wave.giantAmount)
            {
                int i = Random.Range(0, wave.zombies.Length);
                int spawn = Random.Range(0, spawnPoints.Length);
                Instantiate(wave.zombies[i], spawnPoints[spawn].position, Quaternion.identity);
                if (i == 0)
                    smallZombieCount++;
                else if (i == 1)
                    giantZombieCount++;
                yield return new WaitForSeconds(Random.Range(spawnWait - 3, spawnWait + 1));
            }
            while (smallZombieCount < wave.regularAmount)
            {
                int spawn = Random.Range(0, spawnPoints.Length);
                Instantiate(wave.zombies[0], spawnPoints[spawn].position, Quaternion.identity);
                smallZombieCount++;
                yield return new WaitForSeconds(Random.Range(spawnWait - 3, spawnWait + 1));
            }
            while (giantZombieCount < wave.giantAmount)
            {
                int spawn = Random.Range(0, spawnPoints.Length);
                Instantiate(wave.zombies[1], spawnPoints[spawn].position, Quaternion.identity);
                giantZombieCount++;
                yield return new WaitForSeconds(Random.Range(spawnWait - 3, spawnWait + 1));
            }
        }*/
    }
    #endregion
}
