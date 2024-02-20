using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehaviour : AnimalBehaviour
{
    
    public void Awake()
    {
        LoadData("Turtle");
    }


    public override void Animax()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("Tomate");
    }
    
}
