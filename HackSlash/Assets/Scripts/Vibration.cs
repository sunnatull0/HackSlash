using CandyCoded.HapticFeedback;
using UnityEngine;

public static class Vibration
{
    public static void VibrateMedium()
    {
        if(!VibrationSettings.IsOn)
            return;
        
        HapticFeedback.MediumFeedback();
    }

    public static void VibrateLight()
    {
        if(!VibrationSettings.IsOn)
            return;
        
        HapticFeedback.LightFeedback();
    }

    public static void VibrateHeavy()
    {
        if(!VibrationSettings.IsOn)
            return;
        
        HapticFeedback.HeavyFeedback();
    }
}