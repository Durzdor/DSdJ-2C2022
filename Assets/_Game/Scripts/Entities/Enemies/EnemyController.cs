using System;
using UnityEngine;

[RequireComponent(typeof(LifeController))]
public class EnemyController : MonoBehaviour, IPooleable
{
    [SerializeField] private EnemySO _enemyStats;
    [SerializeField] private LifeUI lifeBarCanvas;
    [SerializeField] private Transform visuals;
    private EnemyModel _enemyModel;
    private LifeController _spLifeController;
    
    
    #region FSM and DT

    [SerializeField] private ObstacleAvoidanceScriptableObject obstacleAvoidance;
    public ObstacleAvoidance Behaviour { get; private set; }

    private bool _waitForIdleState;
    private FSM<EnemyStatesConstants> _fsm;
    private INode _root;
    private bool _previousInSightState;
    private bool _currentInSightState;

    #endregion

    #region Actions

    public event Action<Vector3> OnMove;
    public event Action<Vector3> OnLookAt;
    public event Action OnIdle;
    public event Action OnAttack;

    #endregion

    #region Target

    public CharacterM targetModel;
    private Transform _targetTr => targetModel.transform;

    #endregion


    private void Awake()
    {
        _spLifeController = GetComponent<LifeController>();
        _spLifeController.AssignLife(_enemyStats.maxLife);
        _enemyModel = GetComponent<EnemyModel>();
        InitializeOBS();
    }

    private void Start()
    {
        _spLifeController.OnDie += OnDieCommand;
        _spLifeController.OnTakeDamage += OnTakeDamageCommand;
        _enemyModel.Subscribe(this);
        var lifeCan = Instantiate(lifeBarCanvas, visuals.transform);
        lifeCan.Initialize(_spLifeController);;
        InitDecisionTree();
        InitFsm();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!targetModel.Health.IsAlive()) return;
        _fsm.UpdateState();
    }

    public void OnObjectSpawn()
    {
        _spLifeController.AssignLife(_enemyStats.maxLife);
    }

    private void InitDecisionTree()
    {
        // Actions
        var goToChase = new ActionNode(() => _fsm.Transition(EnemyStatesConstants.Chase));
        var goToAttack = new ActionNode(() => _fsm.Transition(EnemyStatesConstants.Attack));
        var goToIdle = new ActionNode(() => _fsm.Transition(EnemyStatesConstants.Idle));

        // Questions
        var attemptPlayerKill = new QuestionNode(IsCloseEnoughToAttack, goToAttack, goToChase);
        var DidSightChangeToAttack = new QuestionNode(SightStateChanged, goToChase, attemptPlayerKill);
        var IsInSight = new QuestionNode(LastInSightState, DidSightChangeToAttack, attemptPlayerKill);

        var IsPlayerAlive = new QuestionNode(() => targetModel.Health.IsAlive(),  IsInSight,goToIdle);

        _root = IsPlayerAlive;
    }

    private bool SightStateChanged()
    {
        return _currentInSightState != _previousInSightState;
    }

    private bool LastInSightState()
    {
        _previousInSightState = _currentInSightState;
        _currentInSightState = _enemyModel.LineOfSightAI.SingleTargetInSight(_targetTr);
        return _currentInSightState;
    }

    private bool IsCloseEnoughToAttack()
    {
        var distance = Vector3.Distance(transform.position, _targetTr.position);
        // print("Distancia " + distance);
        return distance < _enemyStats.distanceToAttack;
    }

    private bool IsIdleStateCooldown()
    {
        return _waitForIdleState;
    }

    private void SetIdleStateCooldown(bool newState)
    {
        _waitForIdleState = newState;
    }

    private bool CheckPlayerInSight()
    {
        var playerIsInSight = _enemyModel.LineOfSightAI.SingleTargetInSight(_targetTr);
        return playerIsInSight;
    }

    private void InitFsm()
    {
        //--------------- FSM Creation -------------------//                
        // States Creation
        var idle = new EnemyIdleState<EnemyStatesConstants>(_enemyStats.idleTimeLenght, CheckPlayerInSight,
            OnIdleCommand, _root, SetIdleStateCooldown);

        var chase = new EnemyChaseState<EnemyStatesConstants>(_targetTr, _root, Behaviour,
            _enemyStats.attackTimeLenght, OnMoveCommand, OnLookAtCommand, SetIdleStateCooldown);

        var attack = new EnemyAttackState<EnemyStatesConstants>(_root, OnAttackCommand, _enemyStats.attackTimeLenght,
            SetIdleStateCooldown);

        //Idle 
        idle.AddTransition(EnemyStatesConstants.Chase, chase);
        idle.AddTransition(EnemyStatesConstants.Attack, attack);

        //Chase 
        chase.AddTransition(EnemyStatesConstants.Idle, idle);
        chase.AddTransition(EnemyStatesConstants.Attack, attack);

        //Attack
        attack.AddTransition(EnemyStatesConstants.Chase, chase);
        attack.AddTransition(EnemyStatesConstants.Idle, idle);

        _fsm = new FSM<EnemyStatesConstants>(idle);
    }


    #region Commands

    private void OnMoveCommand(Vector3 moveDir)
    {
        OnMove?.Invoke(moveDir);
    }

    private void OnIdleCommand()
    {
        OnIdle?.Invoke();
        SetIdleStateCooldown(true);
    }

    private void OnLookAtCommand(Vector3 dir)
    {
        OnLookAt?.Invoke(dir);
    }

    private void OnAttackCommand()
    {
        OnAttack?.Invoke();
    }

    private void OnDieCommand()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }

    private void OnTakeDamageCommand(float a, float b)
    {
        
    }

    public void AssignTarget(CharacterM data)
    {
        targetModel = data;
    }

    public void InitializeOBS()
    {
        Behaviour = new ObstacleAvoidance(transform, null, obstacleAvoidance.radius,
            obstacleAvoidance.maxObjs, obstacleAvoidance.obstaclesMask,
            obstacleAvoidance.multiplier, targetModel, obstacleAvoidance.timePrediction,
            ObstacleAvoidance.DesiredBehaviour.Seek);
    }

    #endregion
}