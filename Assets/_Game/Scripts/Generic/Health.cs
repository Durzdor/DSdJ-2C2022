using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] [Tooltip("Maximum amount of health")]
    private int maxHealth = 100;

    private Coroutine _dotCoroutine;

    public event Action OnConsumed;
    public event Action OnGained;
    public event Action OnDeath;

    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;
    public int BaseMaxHealth { get; private set; }
    public float GetRatio => CurrentHealth / (float)MaxHealth;
    public int LastDamageTaken { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsInvulnerable { get; private set; }

    private void Awake()
    {
        CurrentHealth = maxHealth;
        IsDead = false;
    }   

    public void Heal(int healAmount)
    {
        var healthBefore = CurrentHealth;
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        // call OnHeal action
        var trueHealAmount = CurrentHealth - healthBefore;
        if (trueHealAmount > 0)
            // use this to display amount healed on screen
            OnGained?.Invoke();
    }

    public void TakeDamage(int damage, float time = 0f)
    {
        if (IsDead || IsInvulnerable) return;
        var healthBefore = CurrentHealth;
        CurrentHealth -= damage;
        SetInvulnerable(time);
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);

        // call OnDamage action
        LastDamageTaken = healthBefore - CurrentHealth;
        if (LastDamageTaken > 0)
            // use this to display on screen
            OnConsumed?.Invoke();

        HandleDeath();
    }

    private void HandleDeath()
    {
        if (IsDead) return;

        // call OnDie action
        if (CurrentHealth <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke();
        }
    }

    private void UnsetInvulnerable()
    {
        IsInvulnerable = false;
    }

    private void SetInvulnerable(float time)
    {
        if (!(time > 0)) return;
        IsInvulnerable = true;
        Invoke(nameof(UnsetInvulnerable), time);
    }

    public void ResetToMax()
    {
        Heal(maxHealth);
        IsDead = false;
    }

    public void BuffMaxHealth(int amount)
    {
        maxHealth += amount;
        Heal(amount);
    }

    public void DeBuffMaxHealth(int amount)
    {
        maxHealth -= amount;
        if (maxHealth >= CurrentHealth) return;
        CurrentHealth = maxHealth;
        OnConsumed?.Invoke();
    }

    public void SetStartingMaxHealth(int amount)
    {
        maxHealth = amount;
        BaseMaxHealth = amount;
        ResetToMax();
    }

    private IEnumerator DamageOverTimeCor(float duration, float interval, int damage)
    {
        var currT = 0f;
        var totalT = 0f;
        while (totalT < duration)
        {
            while (currT < interval)
            {
                currT += Time.deltaTime;
                totalT += Time.deltaTime;
                yield return null;
            }

            currT = 0;
            TakeDamage(damage);
            yield return null;
        }
    }

    public void DealDamageOvertime(float duration, float interval, int damage)
    {
        if (_dotCoroutine != null) StopCoroutine(_dotCoroutine);
        _dotCoroutine = StartCoroutine(DamageOverTimeCor(duration, interval, damage));
    }
}