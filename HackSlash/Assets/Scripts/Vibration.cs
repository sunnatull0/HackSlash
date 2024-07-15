using CandyCoded.HapticFeedback;
using UnityEngine;

public static class Vibration
{
    public static void VibrateMedium()
    {
        HapticFeedback.MediumFeedback();
    }

    public static void VibrateLight()
    {
        HapticFeedback.LightFeedback();
        Debug.Log("Vibrated");
    }

    public static void VibrateHeavy()
    {
        HapticFeedback.HeavyFeedback();
    }
}