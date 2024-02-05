using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaBehaviour : AnimalBehaviour
{
    public void Awake()
    {
        LoadData("Panda");
    }

    public override void Animax()
    {
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("Tomate");
    }
}
