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
    [SerializeField] private int _endlessWavesCount = 3;
    [SerializeField] private int _bossWaveInterval = 10;
    [SerializeField] private float _timeBetweenEnemies = 3f;
    [SerializeField] private float _timeBetweenWaves = 5f;

    private int _totalWaveCount;
    private int _currentWaveIndex;
    private bool _bossWave;

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
            HandleMusicForWaveStart();

            if (_currentWaveIndex > _totalWaveCount)
            {
                int randomIndex = Random.Range(_totalWaveCount - _endlessWavesCount, _totalWaveCount);
                yield return SpawnWave(_waves[randomIndex]);
            }
            else
            {
                yield return SpawnWave(_waves[_currentWaveIndex - 1]);
            }

            HandleMusicForWaveEnd();

            yield return new WaitForSeconds(_timeBetweenWaves);
            _currentWaveIndex++;
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        foreach (var enemyInfo in wave.EnemyInfos)
        {
            for (int i = 0; i < enemyInfo.Count; i++)
            {
                Instantiate(enemyInfo.EnemyPrefab, SpawnPointsManager.Instance.GetRandomSpawnPoint(enemyInfo).position, Quaternion.identity);
                yield return new WaitForSeconds(_timeBetweenEnemies);
            }
        }
        yield return new WaitUntil(AllEnemiesDestroyed);
    }

    private void HandleMusicForWaveStart()
    {
        if (_currentWaveIndex % _bossWaveInterval == 0)
        {
            _bossWave = true;
            BackgroundAudio.Instance.StopAmbienceMusic();
            BackgroundAudio.Instance.StopRegularMusic();
            BackgroundAudio.Instance.PlayBossMusic();
        }
    }

    private void HandleMusicForWaveEnd()
    {
        if (_bossWave)
        {
            BackgroundAudio.Instance.StopBossMusic();
            BackgroundAudio.Instance.PlayAmbienceMusic();
            BackgroundAudio.Instance.PlayRegularMusic();
            _bossWave = false;
        }
    }

    private bool AllEnemiesDestroyed()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
