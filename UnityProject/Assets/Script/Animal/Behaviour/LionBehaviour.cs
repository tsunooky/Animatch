using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class LionBehaviour : AnimalBehaviour
{
    
    public void Awake()
    {
        LoadData("Lion");
        DefAnimax += "Heals all his team up to 15hp max each.";
        DefPassive += "When receiving a heal, the heal is 150%.";
    }
    
    public override void Animax()
    {
        foreach (AnimalBehaviour animal in player.animaux_vivant)
        {
            animal.Soin(15);
            animal.healthBar.SetHealth(animal.pv);
        }
        player.enAction = false;
    }

    public override void Soin(int heal) 
    {
        base.Soin(heal + heal/2);
    }
}

