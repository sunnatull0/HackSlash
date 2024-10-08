using UnityEngine;
using UnityEngine.UI;

public class ButtonResizer : MonoBehaviour
{
    [SerializeField] private RectTransform buttonRect;
    [SerializeField] private Vector2 minSize;
    [SerializeField] private Vector2 maxSize;
    private Slider hudPositionSlider;
    
    private void Start()
    {
        hudPositionSlider = UIManager.Instance.ControllerSlider;
        hudPositionSlider.onValueChanged.AddListener(UpdateButtonSize);
        UpdateButtonSize(hudPositionSlider.value); // Initialize button size
    }

    private void UpdateButtonSize(float value)
    {
        buttonRect.sizeDelta = Vector2.Lerp(minSize, maxSize, value);
    }
}