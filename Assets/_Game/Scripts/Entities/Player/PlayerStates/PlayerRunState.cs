using System;
using  UnityEngine;

public class PlayerRunState<T> : State<T>
{
    private T _inputIdle;
    private T _inputAttack;
    private iInput _playerInput;
    private Action<Vector3> _onMove;


    public PlayerRunState(T inputIdle, T inputAttack,iInput playerInput, Action<Vector3> onMove)
    {
        _inputIdle = inputIdle;
        _playerInput = playerInput;
        _inputAttack = inputAttack;
        _onMove = onMove;
    }
    public override void Execute()
    {
        _playerInput.UpdateInputs();
        var h = _playerInput.GetH;
        var v = _playerInput.GetV;
        
        if (_playerInput.IsRunning())
        {
                Vector3 dir = new Vector3(h,0,v);
                _onMove?.Invoke(dir);
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
