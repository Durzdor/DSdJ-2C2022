using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private CharacterStatsSO baseCharacterStats;

    [Header("Current Modifiers")] [Space(5)] [SerializeField]
    private StatFloatDictionary floatDictionaryBuffs = new StatFloatDictionary();

    [SerializeField] private StatFloatDictionary floatDictionaryDeBuffs = new StatFloatDictionary();
    [SerializeField] private StatFloatDictionary floatDictionaryTempBuffs = new StatFloatDictionary();
    [SerializeField] private StatFloatDictionary floatDictionaryTempDeBuffs = new StatFloatDictionary();
    [SerializeField] private StatIntDictionary intDictionaryBuffs = new StatIntDictionary();
    [SerializeField] private StatIntDictionary intDictionaryDeBuffs = new StatIntDictionary();
    [SerializeField] private StatIntDictionary intDictionaryTempBuffs = new StatIntDictionary();
    [SerializeField] private StatIntDictionary intDictionaryTempDeBuffs;
    private void InitializeDictionaries(){}
    private Coroutine _modifierCoroutine;

    #region Min Total Variable Value

    // Min Variable Amounts
    private const int MIN_MAX_HEALTH = 1;
    private const int MIN_SPREAD = 0;
    private const int MIN_DAMAGE = 0;
    private const int MIN_PIERCE = 0;
    private const float MIN_FIRE_RATE = 0.1f;
    private const float MIN_KNOCK_BACK = 0f;
    private const float MIN_MOVE_SPEED = 0f;
    private const float MIN_DISRUPTION = 0f;
    private const float MIN_VOLLEY_AREA = 0f;

    #endregion

    public int MaxHealth => CalculateTotalMaxHealth();
    public int TotalSpread => CalculateTotalSpread();
    public int TotalDamage => CalculateTotalDamage();
    public int TotalPierce => CalculateTotalPierce();
    public float TotalFireRate => CalculateTotalFireRate();
    public float TotalKnockBack => CalculateTotalKnockBack();
    public float TotalMoveSpeed => CalculateTotalMoveSpeed();
    public float TotalDisruption => CalculateTotalDisruption();
    public float TotalVolleyArea => CalculateTotalVolleyArea();
    public float DisruptionDuration => 1f;
    public float VolleyDuration => 1f;

    private void Start()
    {
        SetupStatDictionaries();
        ResetAllModifiers();
    }

    private int CalculateTotalMaxHealth()
    {
        var buffs = intDictionaryBuffs[StatNames.MaxHealthI] + intDictionaryTempBuffs[StatNames.MaxHealthI];
        var deBuffs = intDictionaryDeBuffs[StatNames.MaxHealthI] + intDictionaryTempDeBuffs[StatNames.MaxHealthI];
        var minValue = Mathf.Max(baseCharacterStats.MaxHealth + buffs - deBuffs, MIN_MAX_HEALTH);
        return Mathf.Min(minValue, 999);
    }

    private int CalculateTotalSpread()
    {
        var buffs = intDictionaryBuffs[StatNames.SpreadI] + intDictionaryTempBuffs[StatNames.SpreadI];
        var deBuffs = intDictionaryDeBuffs[StatNames.SpreadI] + intDictionaryTempDeBuffs[StatNames.SpreadI];
        var minValue = Mathf.Max(baseCharacterStats.Spread + buffs - deBuffs, MIN_SPREAD);
        return Mathf.Min(minValue, 30);
    }

    private int CalculateTotalDamage()
    {
        var buffs = intDictionaryBuffs[StatNames.DamageI] + intDictionaryTempBuffs[StatNames.DamageI];
        var deBuffs = intDictionaryDeBuffs[StatNames.DamageI] + intDictionaryTempDeBuffs[StatNames.DamageI];
        var minValue = Mathf.Max(baseCharacterStats.Damage + buffs - deBuffs, MIN_DAMAGE);
        return Mathf.Min(minValue, 999);
    }

    private int CalculateTotalPierce()
    {
        var buffs = intDictionaryBuffs[StatNames.PierceI] + intDictionaryTempBuffs[StatNames.PierceI];
        var deBuffs = intDictionaryDeBuffs[StatNames.PierceI] + intDictionaryTempDeBuffs[StatNames.PierceI];
        var minValue = Mathf.Max(baseCharacterStats.Pierce + buffs - deBuffs, MIN_PIERCE);
        return Mathf.Min(minValue, 999);
    }

    private float CalculateTotalFireRate()
    {
        var buffs = floatDictionaryBuffs[StatNames.FireRateF] + floatDictionaryTempBuffs[StatNames.FireRateF];
        var deBuffs = floatDictionaryDeBuffs[StatNames.FireRateF] + floatDictionaryTempDeBuffs[StatNames.FireRateF];
        var minValue = Mathf.Max(baseCharacterStats.FireRate - buffs + deBuffs, MIN_FIRE_RATE);
        return Mathf.Min(minValue, 999);
    }

    private float CalculateTotalKnockBack()
    {
        var buffs = floatDictionaryBuffs[StatNames.KnockBackF] + floatDictionaryTempBuffs[StatNames.KnockBackF];
        var deBuffs = floatDictionaryDeBuffs[StatNames.KnockBackF] + floatDictionaryTempDeBuffs[StatNames.KnockBackF];
        var minValue = Mathf.Max(baseCharacterStats.KnockBack + buffs - deBuffs, MIN_KNOCK_BACK);
        return Mathf.Min(minValue, 999);
    }

    private float CalculateTotalMoveSpeed()
    {
        var buffs = floatDictionaryBuffs[StatNames.MoveSpeedF] + floatDictionaryTempBuffs[StatNames.MoveSpeedF];
        var deBuffs = floatDictionaryDeBuffs[StatNames.MoveSpeedF] + floatDictionaryTempDeBuffs[StatNames.MoveSpeedF];
        var minValue = Mathf.Max(baseCharacterStats.MoveSpeed + buffs - deBuffs, MIN_MOVE_SPEED);
        return Mathf.Min(minValue, 20);
    }

    private float CalculateTotalDisruption()
    {
        var buffs = floatDictionaryBuffs[StatNames.DisruptionF] + floatDictionaryTempBuffs[StatNames.DisruptionF];
        var deBuffs = floatDictionaryDeBuffs[StatNames.DisruptionF] + floatDictionaryTempDeBuffs[StatNames.DisruptionF];
        var minValue = Mathf.Max(baseCharacterStats.Disruption + buffs - deBuffs, MIN_DISRUPTION);
        return Mathf.Min(minValue, 20);
    }

    private float CalculateTotalVolleyArea()
    {
        var buffs = floatDictionaryBuffs[StatNames.VolleyAreaF] + floatDictionaryTempBuffs[StatNames.VolleyAreaF];
        var deBuffs = floatDictionaryDeBuffs[StatNames.VolleyAreaF] + floatDictionaryTempDeBuffs[StatNames.VolleyAreaF];
        var minValue = Mathf.Max(baseCharacterStats.Volley + buffs - deBuffs, MIN_VOLLEY_AREA);
        return Mathf.Min(minValue, 20);
    }

    [ContextMenu("Setup Dictionaries")]
    private void SetupStatDictionaries()
    {
        var originalFloat = baseCharacterStats.FloatStatsDic;
        var originalInt = baseCharacterStats.IntStatDic;
        var floatDicList = new List<StatFloatDictionary>
            { floatDictionaryBuffs, floatDictionaryDeBuffs, floatDictionaryTempBuffs, floatDictionaryTempDeBuffs };
        var intDicList = new List<StatIntDictionary>
            { intDictionaryBuffs, intDictionaryDeBuffs, intDictionaryTempBuffs, intDictionaryTempDeBuffs };
        foreach (var fDic in floatDicList) CopyDictionaryFloat(originalFloat, fDic);
        foreach (var iDic in intDicList) CopyDictionaryInt(originalInt, iDic);
    }

    public void ChangeModifier(StatNames stat, bool isBuff, int intValue = int.MinValue,
        float floatValue = float.MinValue, bool isTemporal = false, float tempDuration = 0f)
    {
        if (tempDuration > 0)
        {
            if (_modifierCoroutine != null)
                StopCoroutine(_modifierCoroutine);
            else
                StartCoroutine(ModifierCountdown(tempDuration, stat, isBuff, intValue, floatValue, isTemporal));
        }

        if (isBuff)
        {
            if (intValue != int.MinValue)
            {
                if (isTemporal)
                    IntToDictionaryKey(intDictionaryTempBuffs, stat, intValue);
                else
                    AddIntToDictionaryKey(intDictionaryBuffs, stat, intValue);
            }

            if (!(floatValue > float.MinValue)) return;
            if (isTemporal)
                FloatToDictionaryKey(floatDictionaryTempBuffs, stat, floatValue);
            else
                AddFloatToDictionaryKey(floatDictionaryBuffs, stat, floatValue);
        }
        else
        {
            if (intValue != int.MinValue)
                if (isTemporal)
                    IntToDictionaryKey(intDictionaryTempDeBuffs, stat, intValue);
                else
                    AddIntToDictionaryKey(intDictionaryDeBuffs, stat, intValue);

            if (!(floatValue > float.MinValue)) return;
            if (isTemporal)
                FloatToDictionaryKey(floatDictionaryTempDeBuffs, stat, floatValue);
            else
                AddFloatToDictionaryKey(floatDictionaryDeBuffs, stat, floatValue);
        }
    }

    private IEnumerator ModifierCountdown(float duration, StatNames stat, bool isBuff, int intValue, float floatValue,
        bool isTemporal)
    {
        var currCount = 0f;
        while (currCount <= duration)
        {
            currCount += Time.deltaTime;
            yield return null;
        }

        var nInt = intValue == int.MinValue ? int.MinValue : 0;
        var nFloat = floatValue > float.MinValue ? 0 : float.MinValue;
        ChangeModifier(stat, isBuff, nInt, nFloat, isTemporal);
    }

    [ContextMenu("ResetModifiers")]
    public void ResetAllModifiers()
    {
        ResetBuffs();
        ResetDeBuffs();
    }

    public void ResetBuffs()
    {
        ResetIntDic(intDictionaryBuffs);
        ResetIntDic(intDictionaryTempBuffs);
        ResetFloatDic(floatDictionaryBuffs);
        ResetFloatDic(floatDictionaryTempBuffs);
    }

    public void ResetDeBuffs()
    {
        ResetIntDic(intDictionaryDeBuffs);
        ResetIntDic(intDictionaryTempDeBuffs);
        ResetFloatDic(floatDictionaryDeBuffs);
        ResetFloatDic(floatDictionaryTempDeBuffs);
    }

    private void ResetIntDic(StatIntDictionary intDictionary)
    {
        foreach (var pair in baseCharacterStats.IntStatDic)
        {
            var key = pair.Key;
            if (intDictionary.ContainsKey(key)) intDictionary[key] = 0;
        }
    }

    private void ResetFloatDic(StatFloatDictionary floatDictionary)
    {
        foreach (var pair in baseCharacterStats.FloatStatsDic)
        {
            var key = pair.Key;
            if (floatDictionary.ContainsKey(key)) floatDictionary[key] = 0;
        }
    }

    private void CopyDictionaryInt(StatIntDictionary original, StatIntDictionary copied)
    {
        foreach (var pair in original)
        {
            var pairKey = pair.Key;
            if (!copied.ContainsKey(pairKey)) copied.Add(pair);
        }
    }

    private void CopyDictionaryFloat(StatFloatDictionary original, StatFloatDictionary copied)
    {
        foreach (var pair in original)
        {
            var pairKey = pair.Key;
            if (!copied.ContainsKey(pairKey)) copied.Add(pair);
        }
    }

    private void AddIntToDictionaryKey(StatIntDictionary intDictionary, StatNames key, int valueAdded)
    {
        if (intDictionary.ContainsKey(key))
            intDictionary[key] += valueAdded;
    }

    private void IntToDictionaryKey(StatIntDictionary intDictionary, StatNames key, int valueAdded)
    {
        if (intDictionary.ContainsKey(key))
            intDictionary[key] = valueAdded;
    }

    private void AddFloatToDictionaryKey(StatFloatDictionary floatDictionary, StatNames key, float valueAdded)
    {
        if (floatDictionary.ContainsKey(key))
            floatDictionary[key] += valueAdded;
    }

    private void FloatToDictionaryKey(StatFloatDictionary floatDictionary, StatNames key, float valueAdded)
    {
        if (floatDictionary.ContainsKey(key))
            floatDictionary[key] = valueAdded;
    }
}