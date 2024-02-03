using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaBehaviour : AnimalBehaviour
{
    public void Awake()
    {
        prefabBulletSpawn = Resources.Load<GameObject>("Prefabs/BulletSpawn");
        LoadData("Panda");
    }

    public override void LancerPouvoir()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("Tomate");
    }
}
