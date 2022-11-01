using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable<T>
{
    string Name { get; }
    T Owner { get; }
    public void BePicked(T _picker);
    
}
