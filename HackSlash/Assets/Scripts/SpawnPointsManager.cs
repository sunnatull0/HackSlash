using System.Collections.Generic;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPointsManager : MonoBehaviour
{
    public static SpawnPointsManager Instance;

    public List<Transform> SpawnPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform GetRandomSpawnPoint(EnemySpawnInfo enemyInfo)
    {
        var validSpawnPoints = SpawnPoints;

        if (enemyInfo.PositionType == EnemySpawnInfo.SpawnPositionType.Bottom)
        {
            validSpawnPoints = SpawnPoints.Count > 1 ? SpawnPoints.GetRange(0, 2) : SpawnPoints;
        }

        return validSpawnPoints[Random.Range(0, validSpawnPoints.Count)];
    }

}