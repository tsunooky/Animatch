using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class DogBehaviour : AnimalBehaviour, Tireur
{
    
    public void Awake()
    {
        LoadData("Dog");
        potentielleprojDataAnimax = Resources.Load<ProjectileData>("Data/Projectile/Os");
        DefAnimax += "Throws a bone and teleports where it lands.";
        DefPassive += "Uppon death, bites in front of him.";
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
    
}
