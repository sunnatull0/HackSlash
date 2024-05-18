using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    private class Wave
    {
        public List<EnemySpawnInfo> EnemyInfos;
    }

    public static event Action<int> OnWaveStarted;

    [SerializeField] private List<Wave> _waves;
    [SerializeField] private int _endlessWavesCount = 3; // Number of waves to play endlessly after finishing all waves
    [SerializeField] private float _timeBetweenEnemies = 3f;
    [SerializeField] private float _timeBetweenWaves = 5f;

    private int _totalWaveCount;
    private int _currentWaveIndex;

    private void Start()
    {
        _totalWaveCount = _waves.Count;
        _currentWaveIndex = 1;

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            OnWaveStarted?.Invoke(_currentWaveIndex);
            
            // If all waves are played, play random waves from the last N waves
            if (_currentWaveIndex > _totalWaveCount)
            {
                int randomIndex = Random.Range(_totalWaveCount - _endlessWavesCount + 1, _totalWaveCount + 1);
                yield return SpawnWave(_waves[randomIndex - 1]);

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
            else // Otherwise, play the next wave
            {
                yield return SpawnWave(_waves[_currentWaveIndex - 1]);

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
            _currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        foreach (var enemyInfo in wave.EnemyInfos)
        {
            for (int i = 0; i < enemyInfo.Count; i++)
            {
                // Spawn enemies
                Instantiate(enemyInfo.EnemyPrefab, SpawnPointsManager.Instance.GetRandomSpawnPoint(enemyInfo).position,
                    Quaternion.identity);
                yield return new WaitForSeconds(_timeBetweenEnemies);
            }
        }

        // Wait until all enemies from the wave are destroyed
        yield return new WaitUntil(AllEnemiesDestroyed);
    }

    private bool AllEnemiesDestroyed()
    {
        const string enemyTag = "Enemy";

        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        return enemies.Length == 0;
    }
}