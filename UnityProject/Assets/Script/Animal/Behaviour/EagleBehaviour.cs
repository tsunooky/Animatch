using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using Destructible2D.Examples;

public class EagleBehaviour : AnimalBehaviour, Tireur
{
    private int Bump;
    private bool BOOM;
    private GameObject Explosion;
    
    public void Awake()
    {
        Explosion = Resources.Load<ProjectileData>("Data/Projectile/Canon").Explosion;
        BOOM = false;
        Bump = 10;
        LoadData("Eagle");
        DefAnimax += "Jump and deal damage when it lands.";
        DefPassive += "It shows a better trajectory for the projectile.";
    }

    public override void Animax()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<ClickBehaviour>().Initialize(this);
    }
    
    public void SpellAfterClick()
    {
        GameManager.Instance.playerActif.animalActif.gameObject.AddComponent<AimBehviour>().Initialize(this);
    }

    public void SpellAfterShoot(Vector2 startPosition ,Vector2 currentMousePos)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = (startPosition - currentMousePos) * (Bump * 0.075f);
        player.enAction = false;
        StartCoroutine(activateBoom());
    }

    private IEnumerator activateBoom()
    {
        yield return new WaitForSeconds((float)0.5);
        BOOM = true;
    }
    
    void OnCollisionEnter2D(Collision2D collison2D)
    {
        // Vérifiez si la collision concerne un animal
        if (collison2D.gameObject.CompareTag("Explosion") && !ListDegat.Contains((collison2D,collison2D.gameObject.transform.position)))
        {
            D2dExplosion explosion = collison2D.gameObject.GetComponent<D2dExplosion>();
            Degat(explosion.degat);
            
            ListDegat.Add((collison2D,collison2D.gameObject.transform.position));
        }

        if (BOOM)
        {
            BOOM = false;
            GameObject clone = Instantiate(Explosion, transform.position, transform.rotation);
            //ListDegat.Add((clone.GetComponent<Collision2D>(),clone.transform.position));
            clone.SetActive(true);
            // DEGAT DE L'atterisage
            clone.GetComponent<D2dExplosion>().degat = 0;
        }
    }
}

