using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Skill[] skillsPrefabs;
    [SerializeField] private List<Skill> currentSkills = new List<Skill>();
    [SerializeField] private HabilityUI skillCanvas;

    private void Start()
    {
        var canvas = Instantiate(skillCanvas, transform);
        foreach (var item in skillsPrefabs)
        {
            var skill = Instantiate(item, canvas.Visuals);
            currentSkills.Add(skill);
        }
    }

    public void UseSkill(int index)
   {
       currentSkills[index].ActivateSkill();
   }
}