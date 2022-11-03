using System;
using System.Collections;
using UnityEngine;

public class PlayerModel : Actor
{
  #region Position/Physics
    
    private Transform _transform;
    private Rigidbody _rb;
    private float Vel => _rb.velocity.magnitude;
    private PlayerView _view;
    [SerializeField]public PlayerData data;
 
    public event Action OnDead;
    public event Action<int> OnHit;
    private bool isAlive;
    #endregion

    private void Awake()
    {
        BakeReferences();
        isAlive = true;
    }

    void BakeReferences()
    {
        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        _view = GetComponent<PlayerView>();
    }

    public void SubscribeEvents(PlayerController controller)
    {
        controller.OnAttack += Attack;
        controller.OnMove   += Move;
        controller.OnIdle   += Idle;
        controller.OnDie   += Die;
        controller.OnLookAt += LookAt;
    }

    public void Idle()
    {
        _rb.velocity=Vector3.zero;
        _view.IdleAnimation();
    }

    public void Move(Vector3 dir)
    {   
        var finalSpeed = data.walkSpeed; // cambiar por si hay más de una velocidad
        var currDir = dir * finalSpeed;
        
        _rb.velocity = currDir;
        _view.RunAnimation(currDir.normalized);
    }


    public void Attack(int dmgModifier)
    {
        _view.AttackAnimation();
        var attackWait = AttackWait(0.9f);
        StartCoroutine(attackWait);
    }
    

    IEnumerator AttackWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopCoroutine(AttackWait(0));
    }
    public void Die()
    {
        isAlive = false;
        _view.DeadAnimation();
    }


    public void LookAt(Vector3 dir)
    {
        var look = dir.normalized.magnitude;
        if (look==0f) return;
        dir.y = _transform.position.y;
        _transform.forward = dir.normalized;
    }

    #region Attack

    public void TakeDamage(int damage)
    {
        OnHit?.Invoke(damage);
    }

    #endregion

    public void RealDead()
    {
        OnDead?.Invoke();
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

}
