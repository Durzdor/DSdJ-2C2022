using System;
using UnityEngine;
using UnityEngine.Events;

public class HabilityUI : CanvasFiller
{
    [SerializeField] private Transform visuals;
    public Transform Visuals => visuals;
    public void Initialize(Skill controller)
    {
        isPermanent = true;
        UpdateCanvas(controller.SkillCooldown,controller.SkillCooldown);
        if(isPermanent){return;}
        ClearLifeBar();
    }
    
}