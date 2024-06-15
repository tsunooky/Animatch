using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleBehaviour : AnimalBehaviour
{

    private bool invinsible; 
    public float yIncreaseAmount = 0.5f;
    
    public void Awake()
    {
        invinsible = false;
        LoadData("Turtle");
    }


    public override void Animax()
    {
        float currentZRotation = transform.eulerAngles.z;
        invinsible = true;
        if (currentZRotation > 80 && currentZRotation < 300)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y + yIncreaseAmount, transform.position.z);
        }
        player.enAction = false;
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
