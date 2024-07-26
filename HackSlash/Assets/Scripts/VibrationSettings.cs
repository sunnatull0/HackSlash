using UnityEngine;

public class VibrationSettings : MonoBehaviour
{
    public static bool IsOn;

    private void OnEnable()
    {
        Debug.Log(IsOn);
    }

    public void Toggle()
    {
        IsOn = !IsOn;
    }
}
