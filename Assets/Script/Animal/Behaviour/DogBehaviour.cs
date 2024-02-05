using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : AnimalBehaviour
{
    
    public void Awake()
    {
        LoadData("Dog");
    }

    public override void Animax()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("os");
    }
}
