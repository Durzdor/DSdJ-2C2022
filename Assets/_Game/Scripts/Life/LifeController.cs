using UnityEngine;
using System;
using UnityEngine.UI;

[RequireComponent(typeof (LifeUI))]
public class LifeController : MonoBehaviour
{ 
    private float _currentLife;
    private float _maxLife;
    private LifeUI _lifeUI;
    public event Action OnDie;
    public event Action<float,float> OnTakeDamage;
    public Image HealthUI;

    public bool isInmortal;
    public void AssignLife(float data)
    {
        _currentLife = data;
        _maxLife = data;
    }

    private void Awake()
    {
        _lifeUI = GetComponent<LifeUI>();
    }

    private void Start()
    {
        _lifeUI.Initialize(this);
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
        HealthUI.fillAmount = _currentLife / _maxLife;
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