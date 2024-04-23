using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")] [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _inGameCanvas;

    [Header("UI gameobjects")] [SerializeField]
    private GameObject _mainPanel, _settingsPanel, _aboutPanel;

    [SerializeField] private GameObject _gameHUDPanel, _gameOverPanel, _pausePanel, _adPanel;

    [Header("Gameobjects")] [SerializeField]
    private GameObject _player;

    [SerializeField] private GameObject _waves;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // Objects.
        SetActive(_player, false);
        SetActive(_waves, false);

        // Canvases.
        SetActive(_inGameCanvas, false);
        SetActive(_menuCanvas, true);
        
        // UIs.
        SetActive(_mainPanel, true);
        SetActive(_settingsPanel, false);
        SetActive(_aboutPanel, false);
        SetActive(_gameHUDPanel, false);
        SetActive(_gameOverPanel, false);
        SetActive(_pausePanel, false);
        SetActive(_adPanel, false);
    }

    public void OnPlayButtonClicked()
    {
        SetActive(_menuCanvas, false);
        SetActive(_inGameCanvas, true);
        
        SetActive(_mainPanel, false);
        SetActive(_gameHUDPanel, true);
        SetActive(_player, true);
        SetActive(_waves, true);
    }

    public void OnPauseButtonClicked()
    {
        PauseControl.Pause();
    }

    public void OnResumeButtonClicked()
    {
        PauseControl.UnPause();
    }

    private void SetActive(GameObject obj, bool value) => obj.SetActive(value);

}