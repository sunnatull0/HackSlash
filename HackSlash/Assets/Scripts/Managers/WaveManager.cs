using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    private class Wave
    {
        public List<EnemySpawnInfo> EnemyInfos;
    }
    
    [SerializeField] private List<Wave> _waves;

    [SerializeField] private float _timeBetweenEnemies = 3f;
    [SerializeField] private float _timeBetweenWaves = 5f;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        foreach (var wave in _waves)
        {
            foreach (var enemyInfo in wave.EnemyInfos)
            {
                for (int i = 0; i < enemyInfo.Count; i++)
                {
                    Instantiate(enemyInfo.EnemyPrefab,
                        SpawnPointsManager.Instance.GetRandomSpawnPoint(enemyInfo).position, Quaternion.identity);
                    yield return new WaitForSeconds(_timeBetweenEnemies);
                }
            }

            yield return new WaitUntil(AllEnemiesDestroyed);

            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }


    private bool AllEnemiesDestroyed()
    {
        const string enemyTag = "Enemy";

        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        return enemies.Length == 0;
    }
}