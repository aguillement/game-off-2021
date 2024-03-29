using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING, STOPPED };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public float timeBetweenWaves = 5f;

    private int nextWave = 0;
    private SpawnState state = SpawnState.COUNTING;
    private float searchCountdown = 1f;
    private float waveCountdown;
    private Coroutine coroutineSpawner;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state != SpawnState.STOPPED)
        {
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                    WaveCompleted();
                else
                    return;
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                    coroutineSpawner = StartCoroutine(SpawnWave(waves[nextWave]));
            }
            else
                waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length -1)
        {
            nextWave = 0;

            // Stop spawning;
            state = SpawnState.STOPPED;
            StopCoroutine(coroutineSpawner);
        } 
        else
            nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        
        state = SpawnState.WAITING;

        yield break;
    }


    void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, transform.position, transform.rotation);
    }
}
