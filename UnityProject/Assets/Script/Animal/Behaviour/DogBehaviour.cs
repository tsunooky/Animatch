using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class DogBehaviour : AnimalBehaviour, Tireur
{
    private GameObject projectile;
    public void Awake()
    {
        LoadData("Dog");
        potentielleprojDataAnimax = Resources.Load<ProjectileData>("Data/Projectile/Os");
        DefAnimax += "Throws a bone and teleports where it lands.";
        DefPassive += "When it takes damage and has less than 20 hp, his animax regenerates.";
        
    }
    
    public override void Animax()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<ClickBehaviour>().Initialize(this);
    }
    
    public void SpellAfterClick()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<AimBehviour>().Initialize(potentielleprojDataAnimax,this);
    }

    public void SpellAfterShoot(Vector2 startPosition ,Vector2 currentMousePos)
    {
        potentielleprojDataAnimax.Lanceur = gameObject;
        GameObject bullet = Instantiate(potentielleprojDataAnimax.Projectile, startPosition, Quaternion.identity);
        ProjectileBehaviour bulletBehaviour = bullet.GetComponent<ProjectileBehaviour>();
        bulletBehaviour.Set((startPosition, currentMousePos), potentielleprojDataAnimax);
    }
    
    public override void Degat(int damage)
    {
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
        else
        {
            if (pv <= 20)
            {
                AnimaxActivate = false;
            }
        }
        healthBar.SetHealth(pv);
    }
}
