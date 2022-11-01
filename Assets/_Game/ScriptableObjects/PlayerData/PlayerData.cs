using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Entities / Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    [Header ("Movement")]
    public float walkSpeed;
    public float speedFallPenalty;
    public float groundDistance = 0.2f;
    public float jumpHeight;
    public LayerMask groundDetectionList;
    
    [Header("Damage")] 
    public int damage;

    [Header("Life")] public int maxLife;
    
    
    //
    // [SerializeField] private float _speed;
    // [SerializeField] private float _speedFallPen;
    // [SerializeField] private float _jump;
    // [SerializeField] private float groundDistance = 0.2f;
    // [SerializeField] private LayerMask groundDetectionList;

}
