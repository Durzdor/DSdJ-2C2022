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
        
    }

    public void RunAnimation(Vector3 move)
    {
        
    }

    public void AttackAnimation()
    {
        
    }

    public void DeadAnimation()
    {
        
    }

}
