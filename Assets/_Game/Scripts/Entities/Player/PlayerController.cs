using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    private FSM<PlayerStatesEnum> _fsm;
    private PlayerModel _playerModel;
    private PlayerView _playerView;
    private LifeController _playerLifeController;
    private iInput _playerInput;
    private bool isCrouch;
    
    #region Actions
    public event Action<int> OnAttack;
    public event Action<Vector3> OnMove;
    public event Action OnIdle;
    public event Action OnDie;
    public event Action<int> OnHit;
    #endregion
    
    
    private void Awake()
    {
        _playerModel = GetComponent<PlayerModel>();
        _playerInput = GetComponent<iInput>();
        _playerView = GetComponent<PlayerView>();
        _playerLifeController = GetComponent<LifeController>();
        _playerModel.SubscribeEvents(this);
    //    _playerView.SubscribeEvents(this);
    //    _playerLifeController.Subscribe(this);

        isCrouch = false;

        SubscribeEvents();
        
        FsmInit();

    }

    private void FsmInit()
    {
        //--------------- FSM Creation -------------------//                
        var idle = new PlayerIdleState<PlayerStatesEnum>(IdleCommand, PlayerStatesEnum.Run,PlayerStatesEnum.Attack,_playerInput );
        var run = new PlayerRunState<PlayerStatesEnum>( PlayerStatesEnum.Idle, PlayerStatesEnum.Attack,_playerInput,MoveCommand);
        var hit = new PlayerHitState<PlayerStatesEnum>(PlayerStatesEnum.Idle);
        var attack = new PlayerAttackState<PlayerStatesEnum>(PlayerStatesEnum.Idle,PlayerStatesEnum.Run,AttackCommand,1,_playerInput);
        var dead = new PlayerDeadState<PlayerStatesEnum>(DieCommand);

        // Idle State
        idle.AddTransition(PlayerStatesEnum.Run, run);
        idle.AddTransition(PlayerStatesEnum.Attack, attack);
        idle.AddTransition(PlayerStatesEnum.Hit, hit);
        idle.AddTransition(PlayerStatesEnum.Dead, dead);

       
        // Run State
        run.AddTransition(PlayerStatesEnum.Idle, idle);
        run.AddTransition(PlayerStatesEnum.Attack,attack);
        run.AddTransition(PlayerStatesEnum.Hit, hit);
        run.AddTransition(PlayerStatesEnum.Dead, dead);
        
      
        // Attack State
        attack.AddTransition(PlayerStatesEnum.Idle,idle);
        attack.AddTransition(PlayerStatesEnum.Run,run);
        attack.AddTransition(PlayerStatesEnum.Hit, hit);
        attack.AddTransition(PlayerStatesEnum.Dead, dead);
        
        // Hit State
        hit.AddTransition(PlayerStatesEnum.Idle,idle);
        hit.AddTransition(PlayerStatesEnum.Dead, dead);
        
        _fsm = new FSM<PlayerStatesEnum>(idle);
    }

    #region Model Questions


    

    #endregion
    
    
    #region Commands
    public void AttackCommand(int dmg)
    {
        OnAttack?.Invoke(dmg);
    }
    public void MoveCommand(Vector3 dir)
    {
        OnMove?.Invoke(dir);
    }

    public void IdleCommand()
    {
        OnIdle?.Invoke();
    }


    public void HitCommand(int damage)
    {
        _fsm.Transition(PlayerStatesEnum.Hit);
        OnHit?.Invoke(damage);   
    }

   
    public void DieCommand()
    {
        OnDie?.Invoke();
        
        _fsm.Transition(PlayerStatesEnum.Dead);
    }
    public void DeadBrain(){
        _fsm.Transition(PlayerStatesEnum.Dead);
    }
    #endregion
    private void Update()
    {
        if (_playerModel != null) 
        {
            _fsm.UpdateState();
            
        }
    }

    void SubscribeEvents()
    {
     //   _playerModel.OnHit += HitCommand;
        _playerLifeController.OnDie += DieCommand;
    }

}
