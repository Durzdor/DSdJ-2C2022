using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStatPicker : MonoBehaviour
{
    [SerializeField] private GenericStatModifier[] posibleStats;
    void Start()
    {
        var index = Random.Range(0, posibleStats.Length);
        var stat= Instantiate(posibleStats[index],transform);
    }
}
