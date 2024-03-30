using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class BossLogic : MonoBehaviour
    {
        [SerializeField] private GameObject _childEnemyPrefab;
        [SerializeField] private float _firstSpawnTime;
        [SerializeField] private float _spawnDelay;
        [SerializeField] private int _enemyLimit;

        [SerializeField] private Transform[] _spawnPoints;
        private int _spawnedEnemies;

        private void Start()
        {
            _spawnedEnemies = 0;
            InvokeRepeating(nameof(SpawnEnemy), _firstSpawnTime, _spawnDelay);
        }


        private void SpawnEnemy()
        {
            Instantiate(_childEnemyPrefab, GetRandomSpawnPoint().position, _childEnemyPrefab.transform.rotation);
            _spawnedEnemies++;

            CheckForLimit();
        }

        
        private void CheckForLimit()
        {
            if (_spawnedEnemies >= _enemyLimit)
            {
                CancelInvoke(nameof(SpawnEnemy));
                Debug.Log("Stopped!");
            }
        }
        
        
        private Transform GetRandomSpawnPoint()
        {
            int index = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[index];
        }
    }
}