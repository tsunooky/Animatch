using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortueBehaviour : AnimalBehaviour
{
    public void Awake()
    {
        LoadData("Tortue");
    }

    public override void LancerPouvoir(GameObject gameObject)
    {
        Debug.Log("Je suis une tres belle tortue qui danse la salsa");
    }
}
