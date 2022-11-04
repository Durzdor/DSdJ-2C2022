using System;
using System.Collections;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
  
  #region Position/Physics
    
    private Transform _transform;
    private Rigidbody _rb;
    private float Vel => _rb.velocity.magnitude;
    private PlayerView _view;
    [SerializeField]public PlayerData data;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyMask;

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
    }

    public void Idle()
    {
        _rb.velocity=Vector3.zero;
        //_view.IdleAnimation();
    }

    public void Move(Vector3 dir)
    {   
     
        var finalSpeed = data.walkSpeed; // cambiar por si hay más de una velocidad
        var currDir = dir * finalSpeed;
        
        _rb.velocity = currDir;
        //  _view.RunAnimation(currDir.normalized.x);
    }


    public void Attack(int dmgModifier)
    {
    //    var moving = _rb.velocity.x;
        
       // _view.Attack(moving);
      //  EnemyHitCheck()?.TakeDamage(data.damage*dmgModifier);
        var attackWait = AttackWait(0.9f);
        StartCoroutine(attackWait);
    }
    

    IEnumerator AttackWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }


    public void Die()
    {
        isAlive = false;
       // _view.DeadAnimation();
    }

    public void LookAt(Vector3 dir)
    {
        _transform.forward = dir.normalized;
    }


    #region Attack

    // private IDamageable EnemyHitCheck()
    // {
    //     var hit = Physics.OverlapSphere(attackPoint.position, attackRadius,enemyMask); //  nonallocate masmejor
    //     
    //     if(hit==null) return null;
    //     return hit.GetComponent<IDamageable>();
    // }

    public void TakeDamage(int damage)
    {
        OnHit?.Invoke(damage);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(attackPoint.position,attackRadius);
    }

    public void RealDead()
    {
        OnDead?.Invoke();
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

}
