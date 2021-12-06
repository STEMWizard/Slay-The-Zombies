using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Gun Stats")]
public class Weapon : ScriptableObject
{
    public float fireRate;
    public float reloadSpeed;
    public int maxAmmo;  
    public int damage;
}
