using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using Destructible2D.Examples;
using Unity.VisualScripting;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public AnimalData animalData;
    public int pv;
    public float poids;
    public int vitesse;
    public PlayerManager player;
    public GameObject prefabBulletSpawn;
    public GameObject bulletSpawn;
    

    private void Start()
    {
        tag = "Animal";
        bulletSpawn = Instantiate(prefabBulletSpawn);
        bulletSpawn.GetComponent<bulletSpawnCheck>().animal = gameObject;
    }
    

    protected void LoadData(string nom_animal)
    {
        animalData = Resources.Load<AnimalData>("Data/Animaux/" + nom_animal);
        pv = animalData.Pv;
        poids = animalData.Poids;
        vitesse = animalData.Vitesse;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void AnimalVisible()
    {
        Rigidbody2D animalRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        animalRigidbody2D.mass = poids;
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.sprite = animalData.sprite;
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.AddComponent<DespawnManager>();
    }
    
    public abstract void LancerPouvoir();

    protected void Degat(int damage)
    {
        pv -= damage;
        if (pv <= 0)
        {
            pv = 0;
            Destroy(gameObject);
        }
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

    private void OnDestroy()
    {
        for (int i = 0; i < player.animaux_vivant.Count ; i++)
        {
            AnimalBehaviour animal = player.animaux_vivant.Dequeue();
            if (animal != this)
            {
                player.animaux_vivant.Enqueue(animal);
            }
        }
        Destroy(bulletSpawn);
    }
}
