using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class LionBehaviour : AnimalBehaviour
{
    
    public void Awake()
    {
        LoadData("Lion");
    }
    
    public override void Animax()
    {
        foreach (AnimalBehaviour animal in player.animaux_vivant)
        {
            //soigne de 20 toutes son équipe
            animal.Soin(20);
            animal.healthBar.SetHealth(animal.pv);
        }
        player.enAction = false;
    }

    public override void Soin(int heal) 
    {
        base.Soin(heal + heal/2);
    }
}

