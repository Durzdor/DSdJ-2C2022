using UnityEngine;
using System;

public class LifeController : MonoBehaviour
{
    private float _currentLife;
    public float CurrentLife => _currentLife;

    private float _maxLife;
    public float MaxLife => _maxLife;
    public event Action OnDie;
    public event Action<float,float> OnTakeDamage; 

    public bool isInmortal;
    public void AssignLife(float data)
    {
        _currentLife = data;
        _maxLife = data;
    }

    public virtual void TakeDamage (float damage)
    {
        if (isInmortal)
        {
            OnTakeDamage?.Invoke(_currentLife, _maxLife);
            return;
        }
        if (_currentLife - damage <= 0)
        {
            Die();
            return;
        }
        _currentLife -= damage;
        OnTakeDamage?.Invoke(_currentLife, _maxLife);
        }
    
    public bool IsAlive()
    {
        return  _currentLife > 0;
    }
    protected virtual void Die()
    {
        OnDie?.Invoke();
    }
}