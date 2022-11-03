using System;
using UnityEngine;

public class PlayerIdleState<T> : State<T>
{
    private T _runInput;
    private T _attackInput;
    private iInput _playerInput;
    private Action _onIdle;
    private Action<Vector3> _onLookAt;

    public PlayerIdleState( Action onIdle, T runInput,
        T attackInput, iInput playerInput, Action<Vector3> onLookAt)
    {
        _runInput = runInput;
        _attackInput = attackInput;
        _playerInput = playerInput;
        _onIdle = onIdle;
        _onLookAt = onLookAt;
    }
    public override void Awake()
    {
        _onIdle?.Invoke();
    }
    public override void Execute()
    {
        _playerInput.UpdateInputs();
        var look = _playerInput.GetLookAt;
        _onLookAt?.Invoke(look);

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