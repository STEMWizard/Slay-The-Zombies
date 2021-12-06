using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave #", menuName = "Wave Configuration", order = 1)]
public class Wave : ScriptableObject
{
    [SerializeField]
    public GameObject[] zombies;
    [SerializeField]
    public int regularAmount, giantAmount;
}
