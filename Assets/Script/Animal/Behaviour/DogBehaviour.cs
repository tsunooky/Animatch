using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : AnimalBehaviour
{
    
    public void Awake()
    {
        prefabBulletSpawn = Resources.Load<GameObject>("Prefabs/BulletSpawn");
        LoadData("Dog");
    }

    public override void LancerPouvoir()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("Tomate");
    }
}
