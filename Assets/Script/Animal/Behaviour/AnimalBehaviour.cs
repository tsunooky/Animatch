using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using Destructible2D.Examples;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public AnimalData animalData;
    public int pv;
    public float poids;
    public int vitesse;

    protected void LoadData(string nom_animal)
    {
        animalData = Resources.Load<AnimalData>("Data/" + nom_animal);
        pv = animalData.Pv;
        poids = animalData.Poids;
        vitesse = animalData.Vitesse;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void AnimalVisible()
    {
        gameObject.AddComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.sprite = animalData.sprite;
        spriteRenderer.transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.AddComponent<CameraManager>();
    }
    
    public abstract void LancerPouvoir(GameObject gameObject);

    protected void Degat(int damage)
    {
        pv -= damage;
        if (pv < 0)
            pv = 0;
    }

    protected void Soin(int heal)
    {
        pv += heal;
    }
    
    // Méthode appelée lorsqu'un autre objet entre en collision avec l'explosion
    void OnCollisionEnter2D(Collision2D collison2D)
    {
        // Vérifiez si la collision concerne un animal
        if (collison2D.gameObject.CompareTag("Explosion"))
        {
            D2dExplosion explosion = collison2D.gameObject.GetComponent<D2dExplosion>();
            Degat(explosion.degat);
        }
    }
}
