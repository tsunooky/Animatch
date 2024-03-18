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
    public GameObject healthBarInstance;
    public float timeSpawn;
    public ProjectileData potentielleprojDataAnimax;
    
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
        healthBarInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Autre/HealthBar"));
        healthBarInstance.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        healthBar = healthBarInstance.GetComponent<HealthBar>();
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
        CircleCollider2D a = gameObject.AddComponent<CircleCollider2D>();
        a.radius *= 0.75f;
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
            Destroy(healthBar);
            Destroy(gameObject);
            if (healthBarInstance != null)
            {
                Destroy(healthBarInstance);
            }
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
        if (healthBarInstance != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y += 30; // Adjust this value as needed
            healthBarInstance.transform.position = screenPosition;
        }
        if (aura != null)
        {
            aura.transform.position = gameObject.transform.position;
        }
    }
}
