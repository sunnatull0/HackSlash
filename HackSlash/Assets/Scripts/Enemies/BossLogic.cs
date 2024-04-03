using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class BossLogic : MonoBehaviour
    {

        [SerializeField] private EnemySpawnInfo _enemySpawnInfo;
        [SerializeField] private float _spawnDelay;

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
    }
}