using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private Collider triggerCollider;

    [Header("Damage over time")] [Space(5)] [SerializeField]
    private bool canDamageOverTime;

    [SerializeField] private float dotInterval = 1;

    private const float INVULNERABILITY_TIME = 0.1f;

    private int _damage;
    private float _currDotInterval;
    private bool _dotCooldown;

    public void TurnCollisionOn()
    {
        triggerCollider.enabled = true;
    }

    public void TurnCollisionOff()
    {
        triggerCollider.enabled = false;
    }

    public void SetDamage(int amount)
    {
        _damage = amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDamageOverTime) return;
        var entity = other.gameObject;
        if (!HasHealth(entity)) return;
        HealthDamage(entity);
    }

    private void OnTriggerStay(Collider other)
    {
        var entity = other.gameObject;
        if (!HasHealth(entity)) return;
        if (!canDamageOverTime) return;
        if (_dotCooldown)
        {
            HealthDamage(entity);
            _dotCooldown = false;
        }
        else
        {
            if (_currDotInterval > 0) _currDotInterval -= Time.deltaTime;
            if (!(_currDotInterval <= 0)) return;
            _dotCooldown = true;
            _currDotInterval = dotInterval;
        }
    }

    private void HealthDamage(GameObject other)
    {
        var hp = other.GetComponent<LifeController>();
        hp.TakeDamage(_damage);
    }

    private bool HasHealth(GameObject go)
    {
        return go.GetComponent<LifeController>() != null;
    }
}