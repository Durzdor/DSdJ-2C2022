using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    private LifeController _lifeController;
    private void Start()
    {
        _lifeController = GetComponent<LifeController>();
    }
    
}
