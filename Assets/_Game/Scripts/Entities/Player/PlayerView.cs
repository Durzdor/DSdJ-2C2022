using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void IdleAnimation()
    {
        _animator.Play("Player Idle");
    }

    public void RunAnimation()
    {
     _animator.Play("Player Run");   
    }

    public void AttackAnimation()
    {
    // _animator.Play("Standing Aim Recoil");   
    }

    public void DeadAnimation()
    {
        
    }

}
