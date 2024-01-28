using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehaviour : AnimalBehaviour
{

    private AimAndShoot aimAndShoot;
    
    public void Awake()
    {
        LoadData("Turtle");
    }


    public override void LancerPouvoir()
    {
        gameObject.AddComponent<AimAndShoot>();
    }
    
}