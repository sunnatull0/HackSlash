using UnityEngine;

public static class PauseControl
{
    public static bool IsPaused { get; private set; }

    public static void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }

    public static void UnPause()
    {
        Time.timeScale = 1;
        IsPaused = false;
    }
}