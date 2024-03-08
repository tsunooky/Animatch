using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : AnimalBehaviour
{
    
    public void Awake()
    {
        LoadData("Dog");
        potentielleprojDataAnimax = Resources.Load<ProjectileData>("Data/Projectile/Os");
    }

    public override void Animax()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize(potentielleprojDataAnimax, 
            potentielleprojDataAnimax.Projectile.GetComponent<Sprite>());
    }
}
