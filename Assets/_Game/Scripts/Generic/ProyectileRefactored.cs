using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ProyectileRefactored : MonoBehaviour
{
    [SerializeField] private LayerMask target;
    [SerializeField] private ProyectileStatsSO statsSo;
    private Rigidbody _rigidbody;
    private Coroutine destroyByTime;
    private int currPierce;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        GetComponent<Collider>().isTrigger = true;
    }

    private void Start()
    {
        currPierce = statsSo.Pierce;
    }

    private void OnEnable()
    {
        CheckLifeSpan();
    }

    private void Update()
    {
        Move();
    }

    private void CheckLifeSpan()
    {
        if(destroyByTime != null){return;}

        destroyByTime = StartCoroutine(DestroyByTime());
    }

    private void Move()
    {
        _rigidbody.velocity = Vector3.forward * statsSo.MoveSpeed;
    }

    private void MakeDamage(LifeController targetLifeController)
    { 
        targetLifeController.TakeDamage(statsSo.Damage);   
    }

    private void OnHit()
    {
        if (currPierce <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        currPierce--;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!LayerCompare.IsGoInLayerMask(collision.gameObject, target)){return;}
        
        MakeDamage(collision.gameObject.GetComponent<LifeController>());
        ApplyKnockBack(collision.ClosestPoint(transform.position),collision.transform);
        OnHit();
        }

    private void ApplyKnockBack(Vector3 contactPoint, Transform collision)
    {
        var targetRb = collision.gameObject.GetComponent<Rigidbody>();
        if(targetRb == null){return;}
        
        var projectileDir = (transform.position - contactPoint).normalized;
        projectileDir *= statsSo.KnockBack * 10000;
        targetRb.AddForce(projectileDir, ForceMode.Force);
    }

    private IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(statsSo.LifeSpan);
        destroyByTime = null;

    }
}