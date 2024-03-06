using System;
using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using Destructible2D.Examples;
using Unity.VisualScripting;
using static Script.Manager.GameManager;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public AnimalData animalData;
    public int pv;
    public float poids;
    public int vitesse;
    public PlayerManager player;
    public GameObject aura;
    public HealthBar healthBar;
    
    public float timeSpawn;
    
    private void Start()
    {
        tag = "Animal";
        timeSpawn = Time.time;
        gameObject.layer = 6;
    }
    

    protected void LoadData(string nom_animal)
    {
        animalData = Resources.Load<AnimalData>("Data/Animaux/" + nom_animal);
        pv = animalData.Pv;
        healthBar = Instantiate(Resources.Load<HealthBar>("Prefabs/Autre/HealthBar"));
        healthBar.SetMaxHealth(pv);
        poids = animalData.Poids;
        vitesse = animalData.Vitesse;   
    }
    
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void AnimalVisible()
    {
        Rigidbody2D animalRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        animalRigidbody2D.angularDrag = 45;
        animalRigidbody2D.mass = poids;
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 5;
        spriteRenderer.sprite = animalData.sprite;
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.AddComponent<DespawnManager>();
        gameObject.AddComponent<Photon.Pun.PhotonTransformView>();
        gameObject.AddComponent<Photon.Pun.PhotonView>();
    }
    
    public abstract void Animax();

    protected void Degat(int damage)
    {
        pv -= damage;
        if (pv <= 0)
        {
            pv = 0;
            healthBar.SetHealth(pv);
            Destroy(gameObject);
        }
        healthBar.SetHealth(pv);
    }

    protected virtual void Soin(int heal)
    {
        pv += heal;
        healthBar.SetHealth(pv);
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
        /*if (collison2D.gameObject.tag == "Map")
        {
            if (Time.time  - timeSpawn < 0.4)
            {
                var vector3 = gameObject.transform.position;
                vector3.y = 10f;
                gameObject.transform.position = vector3;
            }
        }*/
    }

    private void OnDestroy()
    {
        for (int i = 0; i < player.animaux_vivant.Count; i++)
        {
            AnimalBehaviour animal = player.animaux_vivant.Dequeue();
            if (animal != this)
            {
                player.animaux_vivant.Enqueue(animal);
            }
        }

        if (Instance.playerActif.animalActif == this)
        {
            Instance.tourActif = false;
        }
    }

    public void OnMouseEnter()
    {
        if (player.animalActif == this)
        {
            player.enVisee = true;
        }
    }

    public void OnMouseExit()
    {
        player.enVisee = false;
    }


    public void Update()
    {
        healthBar.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y + 0.5f);
        if (aura != null)
        {
            aura.transform.position = gameObject.transform.position;
        }
    }
}
