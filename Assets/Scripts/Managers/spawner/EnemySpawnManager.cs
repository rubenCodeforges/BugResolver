using System;
using System.Collections;
using System.Collections.Generic;
using Managers.spawner;
using UnityEngine;
using Random = System.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public Transform originPoint;
    public float circleRadius;

    public Wave[] waves;
    public float timeBetweenWaves = 5f;

    private int nextWave = 0;
    private float waveCountdown;
    private SpawnState state = SpawnState.COUNTING;
    private float isAliveInterval = 1f;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                OnWaveComplete();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;
        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield return null;
    }

    private bool EnemyIsAlive()
    {
        isAliveInterval -= Time.deltaTime;
        ;
        if (isAliveInterval <= 0f)
        {
            isAliveInterval = 1f;
            AbstractBug[] bugs = FindObjectsOfType<AbstractBug>();
            return Array.TrueForAll(bugs, bug => !bug.isDead);
        }

        return true;
    }

    void OnWaveComplete()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Waves completed");
        }
        else
        {
            nextWave++;            
        }
        
    }

    void SpawnEnemy(AbstractBug enemy)
    {
        var spawned = Instantiate(enemy);
        var position = UnityEngine.Random.insideUnitCircle * circleRadius;
        spawned.transform.position = originPoint.position + new Vector3(position.x, position.y);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(originPoint.position, circleRadius);
    }
}