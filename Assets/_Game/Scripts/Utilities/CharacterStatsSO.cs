using UnityEngine;

[CreateAssetMenu(fileName = "PlayerBaseStats", menuName = "CharacterStats/Player", order = 0)]
public class CharacterStatsSO : ScriptableObject
{
    [SerializeField] private StatIntDictionary intStats = new StatIntDictionary();

        [SerializeField] private StatFloatDictionary floatStats = new StatFloatDictionary();

    public StatIntDictionary IntStatDic => intStats;
    public StatFloatDictionary FloatStatsDic => floatStats;
    public int Spread => intStats[StatNames.SpreadI];
    public int Pierce => intStats[StatNames.PierceI];
    public int Damage => intStats[StatNames.DamageI];
    public int MaxHealth => intStats[StatNames.MaxHealthI];
    public float FireRate => floatStats[StatNames.FireRateF];
    public float Volley => floatStats[StatNames.VolleyAreaF];
    public float KnockBack => floatStats[StatNames.KnockBackF];
    public float Disruption => floatStats[StatNames.DisruptionF];
    public float MoveSpeed => floatStats[StatNames.MoveSpeedF];

    [ContextMenu("Fill Base Stats")]
    private void AddKey()
    {
        intStats.Add(StatNames.SpreadI, 1);
        floatStats.Add(StatNames.FireRateF, 1);
        floatStats.Add(StatNames.VolleyAreaF, 1);
        intStats.Add(StatNames.PierceI, 1);
        floatStats.Add(StatNames.KnockBackF, 1);
        floatStats.Add(StatNames.DisruptionF, 1);
        intStats.Add(StatNames.DamageI, 1);
        intStats.Add(StatNames.MaxHealthI, 1);
        floatStats.Add(StatNames.MoveSpeedF, 1);
    }
}