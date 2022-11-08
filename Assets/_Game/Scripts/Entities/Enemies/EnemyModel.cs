using System;
using UnityEngine;

public class EnemyModel : Actor
{
    public EnemySO data;
    [SerializeField] private LineOfSightAI _lineOfSightAI;
    private Rigidbody _rb;
    private Transform _transform;
    private EnemyView _view;
    public LineOfSightAI LineOfSightAI => _lineOfSightAI;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _view = GetComponent<EnemyView>();
        _view.Subscribe(this);
        _transform = transform;
    }

    public event Action OnRun;
    public event Action OnAttack;

    public void Subscribe(EnemyController controller)
    {
        controller.OnMove += Move;
        controller.OnAttack += Attack;
        controller.OnIdle += Idle;
        controller.OnLookAt += LookAt;
    }

    private void Idle()
    {
        _rb.velocity = Vector3.zero;
    }

    private void Move(Vector3 dir)
    {
        var dirNorm = dir.normalized;
        _rb.velocity = dirNorm * data.speed;

        OnRun?.Invoke();
    }

    private void LookAt(Vector3 dir)
    {
        _transform.forward = dir.normalized;
    }

    private void Attack()
    {
        OnAttack?.Invoke();
    }

    private void Die()
    {
    }
}