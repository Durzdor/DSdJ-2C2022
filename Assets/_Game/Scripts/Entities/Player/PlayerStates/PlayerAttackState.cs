using System;



public class PlayerAttackState<T> : State<T>
{
    private int _dmg;
    private T _inputIdle;
    private T _inputMove;
    private iInput _playerInput;
    private Action<int> _onAttack;

    public PlayerAttackState(T inputIdle, T inputMove, Action<int> onAttack,int dmg, iInput playerInput)
    {
        _onAttack = onAttack;
        _inputIdle = inputIdle;
        _inputMove = inputMove;
        _dmg = dmg;
        _playerInput = playerInput;
    }

    public override void Execute()
    {
        _onAttack?.Invoke(_dmg);
        _playerInput.UpdateInputs();
        
        if (_playerInput.IsRunning())
        {
            parentFSM.Transition(_inputMove);
        }
        else
        {
            parentFSM.Transition(_inputIdle);
        }
        
        
    }
}
