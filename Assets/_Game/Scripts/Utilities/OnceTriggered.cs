using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class OnceTriggered : MonoBehaviour
{
    [SerializeField] private LayerMask contactLayer;
    public UnityEvent triggeredAction;
    private void OnTriggerEnter(Collider other)
    {
        if ((contactLayer & 1 << other.gameObject.layer) != 1 << other.gameObject.layer){return;}
        triggeredAction.Invoke();
    }
}
