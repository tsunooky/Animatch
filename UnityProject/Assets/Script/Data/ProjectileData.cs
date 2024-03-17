using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NouveauProjectile", menuName = "Projectile")]
public class ProjectileData : ScriptableObject
{
    public int Expulsion;
    public int Force;
    public int Degat;
    public GameObject Projectile ; 
    public GameObject Explosion;
    public Sprite SpriteIfHandToHand;
}

