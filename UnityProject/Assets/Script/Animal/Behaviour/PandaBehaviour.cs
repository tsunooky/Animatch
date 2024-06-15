using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class 
    PandaBehaviour : AnimalBehaviour, Tireur
{
    public void Awake()
    {
        LoadData("panda");
        potentielleprojDataAnimax = Resources.Load<ProjectileData>("Data/Projectile/Bamboo");
        DefAnimax += "Grow a Bamboo.";
        DefPassive += "If the Panda doesn't play, it heals itself.";
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
