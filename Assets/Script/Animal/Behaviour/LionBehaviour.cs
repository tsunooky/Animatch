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
        AimAndShoot aimAndShoot = gameObject.AddComponent<AimAndShoot>();
        aimAndShoot.Initialize("Tomate");

        if (GameManager.Instance.joueur.animaux_vivant.Contains(gameObject.GetComponent<LionBehaviour>()))
        {
            Debug.Log("bump tout l'equipe du bot");
        }
        else
        {
            Debug.Log("bump tout l'equipe du joueur");
        }
    }

    protected override void Soin(int heal) // passif lion
    {
        pv += heal + heal/2;
    }
}
