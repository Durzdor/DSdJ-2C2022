using UnityEngine;

[CreateAssetMenu(fileName = "Proyectile", menuName = "Proyectiles", order = 0)]
public class ProyectileStatsSO : ScriptableObject
{
    #region Serializable

    [SerializeField] private int damage;
    [SerializeField] private int pierce;
    [SerializeField] private float knockBack;
    [SerializeField] private float disruption;
    [SerializeField] private float disruptionDuration;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeSpan;
    #endregion
    #region References
    public int Damage => damage;
    public int Pierce => pierce;
    public float KnockBack => knockBack;
    public float Disruption => disruption;
    public float DisruptionDuration => disruptionDuration;
    public float MoveSpeed => moveSpeed;
    public float LifeSpan => lifeSpan;
    #endregion
}