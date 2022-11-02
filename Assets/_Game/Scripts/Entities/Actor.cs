using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamageable, iMobile
{

    public virtual void TakeDamage(int damage)
    {

        
    }

    public virtual void RecoverLife(int _heal)
    {
       
    }
    public virtual void Die()
    {
        throw new NotImplementedException();   
    }

    public virtual void Idle()
    {
        throw new NotImplementedException();
    }

    public virtual void Attack(int dmg)
    {
    }

    public virtual void Jump()
    {
        
    }

    public virtual bool Patrol()
    {
        throw new NotImplementedException();
    }

    public virtual void Chase()
    {
        throw new NotImplementedException();
    }

    public virtual void Move(Vector2 dir)
    {
        throw new NotImplementedException();
    }
    
    
}