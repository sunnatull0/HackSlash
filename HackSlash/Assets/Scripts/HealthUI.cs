using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public static HealthUI Instance;

    [SerializeField] private List<Image> _healthUIs;
    [SerializeField] private Sprite _activeHeartUI;
    [SerializeField] private Sprite _lostHeartUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    [SerializeField] private ParticleSystem _healthEffect;
    public void UpdateHealthUI(float currentHealth)
    {
        for (int i = 0; i < _healthUIs.Count; i++) // Change the color depending on currentHealth.
        {
            _healthUIs[i].sprite = i < currentHealth ? _activeHeartUI : _lostHeartUI;
            _healthEffect.Play();
        }
    }
}