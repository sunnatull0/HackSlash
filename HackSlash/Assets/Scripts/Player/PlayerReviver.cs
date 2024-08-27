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
            Application.targetFrameRate = 60;
            OnPlayerRevive += SpawnPlayer;
        }

        private void OnDestroy()
        {
            OnPlayerRevive -= SpawnPlayer;
        }


        public void Revive()
        {
            OnPlayerRevive?.Invoke();
            Debug.Log("REVIVED!");
        }

        private void SpawnPlayer()
        {
            Instantiate(_playerPrefab, Vector2.zero, _playerPrefab.transform.rotation);
            CameraZoom.Instance.SetPlayerCamera();
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