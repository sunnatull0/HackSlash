using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerHUD : MonoBehaviour
    {
        public static PlayerHUD Instance;

        [SerializeField] private GameObject _hud;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public void DisableHUD() => _hud.SetActive(false);
        public void EnableHUD() => _hud.SetActive(true);
    }
}