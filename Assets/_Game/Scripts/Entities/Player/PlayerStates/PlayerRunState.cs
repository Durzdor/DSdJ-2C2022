using System;
using  UnityEngine;

public class PlayerRunState<T> : State<T>
{
    private T _inputIdle;
    private T _inputAttack;
    private iInput _playerInput;
    private Action<Vector3> _onMove;
    private Action<Vector3> _onLookAt;


    public PlayerRunState(T inputIdle, T inputAttack,iInput playerInput, Action<Vector3> onMove, Action<Vector3> onLookAt)
    {
        _inputIdle = inputIdle;
        _playerInput = playerInput;
        _inputAttack = inputAttack;
        _onMove = onMove;
        _onLookAt = onLookAt;
    }
    public override void Execute()
    {
        _playerInput.UpdateInputs();
        var move = _playerInput.GetMov;
        var look = _playerInput.GetLookAt;
        
        if (_playerInput.IsRunning())
        {
            var dir = move;
            _onMove?.Invoke(dir);
            _onLookAt?.Invoke(look);
        }

        if (_playerInput.IsAttacking())
        {
            parentFSM.Transition(_inputAttack);
        }
        
        if(!_playerInput.IsRunning())
        {
            parentFSM.Transition(_inputIdle); 
        }
        
    }

}
