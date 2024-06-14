using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehaviour : AnimalBehaviour
{

    private bool invinsible; 
    
    public void Awake()
    {
        invinsible = false;
        LoadData("Turtle");
    }


    public override void Animax()
    {
        invinsible = true;
    }

    public override void Degat(int damage)
    {
        if (invinsible)
        {
            invinsible = false;
        }
        else
        {
            if (damage > 25)
                damage = 25;
            pv -= damage;
            if (pv <= 0)
            {
                pv = 0;
                healthBar.SetHealth(pv);
                Destroy(healthBar);
                Destroy(currentInstance);
                if (healthBarInstance != null)
                {
                    Destroy(healthBarInstance);
                }
                Meurt();
            }
            healthBar.SetHealth(pv);
        }
    }
}
