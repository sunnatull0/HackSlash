using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private GameObject _waveUI;
    [SerializeField] private Animator _waveUIanimator;
    [SerializeField] private TMP_Text _waveUItxt;
    private static readonly int ShowWaveUI = Animator.StringToHash("ShowWaveUI");

    private void Start()
    {
        WaveManager.OnWaveStarted += HandleWaveUI;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveStarted -= HandleWaveUI;
    }

    private void HandleWaveUI(int waveIndex)
    {
        UpdateWaveUIText(waveIndex);
        _waveUIanimator.SetTrigger(ShowWaveUI);
    }

    private void UpdateWaveUIText(int index)
    {
        _waveUItxt.text = $"{index} WAVE";
    }
}