using System;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;


public class PlayerDeadState<T> : State<T>
{
    private Action _onDead;
    public PlayerDeadState(Action onDead)
    {
        _onDead = onDead;
    }

    public override void Execute()
    {
        // _onDead?.Invoke();
       
    }
}
