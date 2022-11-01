using System;
using UnityEngine;

public class PlayerHitState<T> : State<T>
{
    private T _idleInput;
    public PlayerHitState(T idleInput)
    {
        _idleInput = idleInput;

    }

    public override void Execute()
    {
        parentFSM.Transition(_idleInput);
    }
}