using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehaviour : AnimalBehaviour
{
    
    public void Awake()
    {
        prefabBulletSpawn = Resources.Load<GameObject>("Prefabs/BulletSpawn");
        LoadData("Turtle");
    }


    public override void LancerPouvoir()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("Tomate");
    }
    
}
