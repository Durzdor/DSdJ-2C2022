using System;
using UnityEngine;

public class PlayerIdleState<T> : State<T>
{
    private T _runInput;
    private T _attackInput;
    private iInput _playerInput;
    private Action _onIdle;


    public PlayerIdleState( Action onIdle, T runInput,
        T attackInput, iInput playerInput)
    {
        _runInput = runInput;
        _attackInput = attackInput;
        _playerInput = playerInput;
        _onIdle = onIdle;
    }

    public override void Execute()
    {
        _playerInput.UpdateInputs();


        if (_playerInput.IsRunning())
        {
            parentFSM.Transition(_runInput);
        }
        else if (_playerInput.IsAttacking())
        {
            parentFSM.Transition(_attackInput);
        }
    }
}