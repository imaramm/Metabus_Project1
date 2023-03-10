using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptable/GZombie_GunData", fileName = "GZombie_GunData")]
public class GZombie_GunData : ScriptableObject
{
    public AudioClip shotClip;
    public AudioClip reloadClip;

    public float damage = 25;

    public int startAmmoRemain = 100;
    public int magCapacity = 25;

    public float timeBetFire = 0.12f;
    public float reloadTime = 1.8f;

}// end of class
