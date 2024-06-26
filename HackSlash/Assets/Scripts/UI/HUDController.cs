using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Slider hudPositionSlider;
    [SerializeField] private GameObject demonstrationPanel;
    [SerializeField] private float demoTime = 1f;

    private Coroutine hidePanelCoroutine;

    private void Start()
    {
        hudPositionSlider.onValueChanged.AddListener(ShowDemonstrationPanel);
        hudPositionSlider.onValueChanged.AddListener((value) =>
        {
            if (hidePanelCoroutine != null)
            {
                StopCoroutine(hidePanelCoroutine);
            }

            hidePanelCoroutine = StartCoroutine(HideDemonstrationPanelAfterDelay());
        });
    }

    private void ShowDemonstrationPanel(float value)
    {
        demonstrationPanel.SetActive(true);
    }

    private IEnumerator HideDemonstrationPanelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(demoTime);
        demonstrationPanel.SetActive(false);
    }
}