using CandyCoded.HapticFeedback;
using UnityEngine;

public static class Vibration
{
    public static void VibrateMedium()
    {
        if(!VibrationSettings.IsOn)
            return;
        
        HapticFeedback.MediumFeedback();
        Debug.Log("Vibrated!");
    }

    public static void VibrateLight()
    {
        if(!VibrationSettings.IsOn)
            return;
        
        HapticFeedback.LightFeedback();
        Debug.Log("Vibrated");
    }

    public static void VibrateHeavy()
    {
        if(!VibrationSettings.IsOn)
            return;
        
        HapticFeedback.HeavyFeedback();
    }
}