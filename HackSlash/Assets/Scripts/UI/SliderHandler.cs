using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private float _tickInterval = 0.1f;

    private float _lastTickValue;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        _lastTickValue = _slider.value;
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        if (Mathf.Abs(value - _lastTickValue) >= _tickInterval)
        {
            SFXManager.Instance.PlaySFX(SFXType.SliderChange);
            _lastTickValue = value;
        }
    }
}