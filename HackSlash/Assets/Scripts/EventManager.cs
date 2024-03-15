using System;

public static class EventManager
{
    public static event Action OnAttack;
    public static event Action OnJump;
    public static event Action OnLand;
    public static event Action OnAttackFinish;

    
    public static void InvokeOnAttackActions()
    {
        OnAttack?.Invoke();
    }
    
    public static void InvokeOnJumpActions()
    {
        OnJump?.Invoke();
    }
    
    public static void InvokeOnLandActions()
    {
        OnLand?.Invoke();
    }
    public static void InvokeOnAttackFinish()
    {
        OnAttackFinish?.Invoke();
    }
    
}
