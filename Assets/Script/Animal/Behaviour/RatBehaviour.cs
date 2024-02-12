using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : AnimalBehaviour
{
    public void Awake()
    {
        LoadData("Rat");
    }

    public override void Animax()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("Tomate");
    }
}

