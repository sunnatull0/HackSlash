using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")] [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _inGameCanvas;

    [Header("UI gameobjects")] 
    [SerializeField] private GameObject _mainPanel, _settingsPanel, _aboutPanel;
    [SerializeField] private GameObject _gameHUDPanel, _gameOverPanel, _pausePanel, _adPanel, _waveUIPanel;
    [SerializeField] private GameObject _demoPanel;

    [Header("Gameobjects")] [SerializeField]
    private GameObject _player;

    [SerializeField] private GameObject _waves;

    [SerializeField] private float _gameOverPanelActivateTime = 1.5f;


    private void Start()
    {
        Init();

        Death.OnPlayerDeath += ActivateGameOverPanel;
    }
    

    private void OnDisable()
    {
        Death.OnPlayerDeath -= ActivateGameOverPanel;
    }

    private void Init()
    {
        // Objects.
        SetActive(_player, false);
        SetActive(_waves, false);

        // Canvases.
        SetActive(_inGameCanvas, true);
        SetActive(_menuCanvas, true);
        
        // UIs.
        SetActive(_mainPanel, true);
        SetActive(_settingsPanel, false);
        SetActive(_aboutPanel, false);
        SetActive(_gameHUDPanel, false);
        SetActive(_gameOverPanel, false);
        SetActive(_pausePanel, false);
        SetActive(_adPanel, false);
        SetActive(_waveUIPanel, true);
        SetActive(_demoPanel, false);
    }

    public void OnPlayButtonClicked()
    {
        ButtonClickEvents.State = GameState.InGame;
        SetActive(_inGameCanvas, true);
        
        SetActive(_mainPanel, false);
        SetActive(_settingsPanel, false);
        SetActive(_aboutPanel, false);
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

    public void RestartGame()
    {
        PauseControl.UnPause();
        SceneManager.LoadScene(0);
    }

    public void ActivateSettingsPanel()
    {
        SetActive(_settingsPanel, true);
    }

    private void ActivateGameOverPanel()
    {
        StartCoroutine(ActivateAfterTime());
    }

    private IEnumerator ActivateAfterTime()
    {
        yield return new WaitForSeconds(_gameOverPanelActivateTime);
        
        SFXManager.Instance.PlaySFX(SFXType.PopUp);
        SetActive(_gameOverPanel, true);
    }
}