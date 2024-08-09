using System;
using UnityEngine;

namespace Player
{
    public class PlayerReviver : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        public static event Action OnPlayerRevive;
        [SerializeField] private bool _go;

        private void Start()
        {
            OnPlayerRevive += SpawnPlayer;
        }

        private void OnDestroy()
        {
            OnPlayerRevive -= SpawnPlayer;
        }


        private void Revive()
        {
            OnPlayerRevive?.Invoke();
        }

        private void SpawnPlayer()
        {
            Instantiate(_playerPrefab, Vector2.zero, _playerPrefab.transform.rotation);
        }

        private void Update()
        {
            if (_go)
            {
                Revive();
                _go = false;
            }
        }
    }
}
