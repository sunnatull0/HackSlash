using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemySpawnInfo
    {
        public enum SpawnPositionType
        {
            Any,
            Bottom
        }

        public SpawnPositionType PositionType;
        public GameObject EnemyPrefab;
        public int Count;
    }
}

