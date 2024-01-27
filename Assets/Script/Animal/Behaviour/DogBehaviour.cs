using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : AnimalBehaviour
{
    public void Awake()
    {
        LoadData("Dog");
    }

    public override void LancerPouvoir()
    {
        gameObject.AddComponent<AimAndShoot>();
    }
}
