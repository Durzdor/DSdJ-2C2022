﻿using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Entities / Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    [Header ("Movement")]
    public float walkSpeed;
  
    [Header("Life")] public int maxLife;
  
}
