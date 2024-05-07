using UnityEngine;

public class ButtonClickEvents : MonoBehaviour
{
    
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _inGameCanvas;

    [SerializeField] private GameObject _mainPanel, _settingsPanel, _aboutPanel;
    [SerializeField] private GameObject _gameHUDPanel, _gameOverPanel, _pausePanel, _adPanel;
    
    
    public static GameState State;

    private void Start()
    {
        State = GameState.Menu;
    }

    public void OnSettingsBackButtonClicked()
    {
        if (State == GameState.Menu)
        {
            _settingsPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }
        else
        {
            _settingsPanel.SetActive(false);
            _pausePanel.SetActive(true);
        }
    }
}
