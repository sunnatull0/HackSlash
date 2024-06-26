using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies
{
    public class BossLogic : MonoBehaviour
    {

        [SerializeField] private EnemySpawnInfo _enemySpawnInfo;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private UnityEvent _onDeath;

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }


        private void SpawnEnemy()
        {
            Instantiate(_enemySpawnInfo.EnemyPrefab,
                SpawnPointsManager.Instance.GetRandomSpawnPoint(_enemySpawnInfo).position,
                Quaternion.identity);
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < _enemySpawnInfo.Count; i++)
            {
                yield return new WaitForSeconds(_spawnDelay);
                SpawnEnemy();
            }
        }

        private void OnDisable()
        {
            _onDeath?.Invoke();
        }
    }
}