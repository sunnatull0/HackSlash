using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action OnAttack;

    public static void InvokeOnAttackActions()
    {
        if (OnAttack != null) OnAttack();
    }
}
