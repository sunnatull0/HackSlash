using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Slider hudPositionSlider;
    
    [SerializeField] private RectTransform _movementButtons;
    [SerializeField] private RectTransform _attackButton;
    
    [Header("Position settings")]
    [SerializeField] private Transform _movementButtonsEdge;
    [SerializeField] private Transform _movementButtonsCenter;
    
    [SerializeField] private Transform _attackButtonEdge;
    [SerializeField] private Transform _attackButtonCenter;
    
    [Header("Demonstration Buttons")]
    [SerializeField] private RectTransform demoMovementButtons;
    [SerializeField] private RectTransform demoAttackButton;
    [SerializeField] private GameObject demonstrationPanel;
    [SerializeField] private float _demoTime = 1f;
    
    private Coroutine hidePanelCoroutine;
    
    private void Start()
    {
        hudPositionSlider.onValueChanged.AddListener(UpdateHUDPosition);
        hudPositionSlider.onValueChanged.AddListener(ShowDemonstrationPanel);
        
        hudPositionSlider.onValueChanged.AddListener((value) =>
        {
            if (hidePanelCoroutine != null)
            {
                StopCoroutine(hidePanelCoroutine);
            }

            hidePanelCoroutine = StartCoroutine(HideDemonstrationPanelAfterDelay());
        });

        UpdateHUDPosition(hudPositionSlider.value);
    }

    private void UpdateHUDPosition(float value)
    {
        _movementButtons.position = Vector2.Lerp(_movementButtonsEdge.position, _movementButtonsCenter.position, value);
        _attackButton.position = Vector2.Lerp(_attackButtonEdge.position, _attackButtonCenter.position, value);
        
        // Update demonstration HUD positions
        demoMovementButtons.position = Vector2.Lerp(_movementButtonsEdge.position, _movementButtonsCenter.position, value);
        demoAttackButton.position = Vector2.Lerp(_attackButtonEdge.position, _attackButtonCenter.position, value);
    }
    
    private void ShowDemonstrationPanel(float value)
    {
        demonstrationPanel.SetActive(true);
    }

    private IEnumerator HideDemonstrationPanelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(_demoTime);
        demonstrationPanel.SetActive(false);
    }
}
