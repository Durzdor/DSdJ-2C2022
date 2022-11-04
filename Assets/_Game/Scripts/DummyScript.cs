using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    private LifeController _lifeController;
    [SerializeField] private float maxLife;
    private Animator animator;
    private Coroutine disappearCoroutine;
    [SerializeField] private float recoveryTime;
    [SerializeField] private GameObject visuals;
    [SerializeField] private ParticleSystem dissapearParticles;
    private void Awake()
    {
        _lifeController = GetComponent<LifeController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _lifeController.AssignLife(maxLife);
        _lifeController.OnDie += Die;
        _lifeController.OnTakeDamage += PlayTakeDamage;
    }

    private void PlayTakeDamage(float a, float b)
    {
        animator.Play("TakeDamage");
    }
    private void Die()
    {
        dissapearParticles.Play();
        if (disappearCoroutine == null)
        {
         disappearCoroutine = StartCoroutine(DisappearTimer());
        }
        _lifeController.AssignLife(maxLife);
    }

    private IEnumerator DisappearTimer()
    {
        visuals.SetActive(false);
        yield return new WaitForSeconds(recoveryTime);
        visuals.SetActive(true);
        disappearCoroutine = null;
    }
}
