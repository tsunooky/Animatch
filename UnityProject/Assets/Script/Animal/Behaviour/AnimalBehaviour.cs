using System;
using System.Collections;
using System.Collections.Generic;
using Destructible2D;
using Script.Manager;
using UnityEngine;
using Photon.Pun;
using Destructible2D.Examples;
using Unity.VisualScripting;
using static Script.Manager.GameManager;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public AnimalData animalData;
    public int pv;
    public int vitesse;
    public PlayerManager player;
    public HealthBar healthBar;
    public GameObject healthBarInstance;
    public float timeSpawn;
    public ProjectileData potentielleprojDataAnimax;
    private GameObject pointeur;
    private SpriteRenderer pointeurSprite;
    public string nom;
    public bool actif;
    public GameObject currentInstance;
    [SerializeField] private ParticleSystem ps = default;
    
    public void setPointeur()
    {
        pointeur = new GameObject($"pointeur de {nom}");
        pointeurSprite = pointeur.AddComponent<SpriteRenderer>();
        pointeurSprite.sprite = Resources.Load<Sprite>("Icons/settings_button");

        // Vérifiez si le sprite a bien été chargé
        if (pointeurSprite.sprite == null)
        {
            Debug.LogError("Sprite 'Icons/settings_button' n'a pas été trouvé dans Resources.");
        }

        pointeurSprite.enabled = false;
    }

    
    private void Start()
    {
        tag = "Animal";
        timeSpawn = Time.time;
        gameObject.layer = 6;
        player = Instance.joueur;
        currentInstance = new GameObject();
    }
    

    protected void LoadData(string nom_animal)
    {
        animalData = Resources.Load<AnimalData>("Data/Animaux/" + nom_animal);
        pv = animalData.Pv;
        vitesse = animalData.Vitesse;   
    }

    public void LoadHealthbar()
    {
        if (player == GameManager.Instance.joueur)
        {
            healthBarInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Autre/HealthBar_blue"));
        }
        else
        {
            healthBarInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Autre/HealthBar_red"));
        }
        
        healthBarInstance.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        healthBar = healthBarInstance.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(pv);
    }

    public void LoadAura()
    {
        if (player == GameManager.Instance.playerActif)
        {
            currentInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Autre/joueur_equi"));
        }
        else
        {
            currentInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Autre/joueur_adv"));
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public void AnimalVisible()
    {
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 5;
        spriteRenderer.sprite = animalData.sprite;
        D2dDestructibleSprite destructibleSprite = gameObject.AddComponent<D2dDestructibleSprite>();
        destructibleSprite.Shape = animalData.sprite;
        destructibleSprite.Rebuild();
        destructibleSprite.RebuildAlphaTex();
        destructibleSprite.Indestructible = true; // L'animal n'est pas destructible
        destructibleSprite.Optimize(); // Optimiser le rendu pour les performances (3 fois)
        destructibleSprite.Optimize();
        destructibleSprite.Optimize();
        destructibleSprite.RebuildAlphaTex();
        destructibleSprite.CropSprite = false;
        D2dSplitter d2dSplitter = gameObject.AddComponent<D2dSplitter>();
        d2dSplitter.Feather = 5;
        D2dPolygonCollider d2dPolygonCollider = gameObject.AddComponent<D2dPolygonCollider>();
        d2dPolygonCollider.Straighten = 0.01f;
        Rigidbody2D animalRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        animalRigidbody2D.angularDrag = 0.05f;
        animalRigidbody2D.mass = 1000f;
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
            Destroy(currentInstance);
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
        Destroy(healthBarInstance);
        Destroy(healthBar);
        Destroy(currentInstance);
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
            screenPosition.y += 50; // Adjust this value as needed
            healthBarInstance.transform.position = screenPosition;
        }

        if (currentInstance != null)
        {
            Vector3 screenPosition = transform.position;
            screenPosition.y += 1.2f; 
            currentInstance.transform.position = screenPosition;
            StartCoroutine(MoveAura());
        }

        actif = player.animalActif == this;
        pointeurSprite.enabled = actif;
        pointeur.transform.position = gameObject.transform.position;
    }
    
    IEnumerator MoveAura()
    {
        while (true)
        {
            // Move the object upwards over 2 seconds
            for (float t = 0; t <= 0.1f; t += Time.deltaTime / 5f)
            {
                currentInstance.transform.position = new Vector3(currentInstance.transform.position.x, currentInstance.transform.position.y + t * 0.02f, currentInstance.transform.position.z);
                yield return null;
            }

            // Move the object downwards over 2 seconds
            for (float t = 0; t <= 0.1f; t += Time.deltaTime / 5f)
            {
                currentInstance.transform.position = new Vector3(currentInstance.transform.position.x, currentInstance.transform.position.y - t * 0.02f, currentInstance.transform.position.z);
                yield return null;
            }
        }
    }
}
