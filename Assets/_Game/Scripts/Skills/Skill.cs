using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [SerializeField] private float skillCooldown;
    public float SkillCooldown => skillCooldown;
    private float currentCooldown;
    private Coroutine cooldownRoutine;
    private bool canShow = false;
    [SerializeField] private Image fillImage;
    private void Update()
    {
        if (canShow)
        {
            currentCooldown -= Time.deltaTime;
            UpdateCanvas(currentCooldown, skillCooldown);
        }
    }

    public void ActivateSkill()
    {
        if (cooldownRoutine == null)
        {
            StartCoroutine(UseCooldown());
            currentCooldown = skillCooldown;
        }
    }
    IEnumerator UseCooldown()
    {
        canShow = true;
        yield return new WaitForSeconds(skillCooldown);
        canShow = false;
        cooldownRoutine = null;
        currentCooldown = skillCooldown;
        UpdateCanvas(currentCooldown, skillCooldown);
        StopCoroutine(UseCooldown());
    }
    private void UpdateCanvas(float currentValue, float maxValue)
    {
        fillImage.fillAmount = currentValue / maxValue;
    }
}