using System.Collections;
using System.Collections.Generic;
using Destructible2D;
using Script.Manager;
using UnityEngine;
using Destructible2D.Examples;
using static Script.Manager.AGameManager;


public abstract class AnimalBehaviour : MonoBehaviour
{
    public AnimalData animalData;
    public int pv;
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
    public bool AnimaxActivate;
    public string DefAnimax = "Animax : \n";
    public string DefPassive = "Passive : \n";


    // Stock les collision2D qui ont infligé degat par chaque projo pour eviter de s'en reprendre un autre
    public List<(Collision2D,Vector3)> ListDegat;
    
    public void setPointeur()
    {
        pointeur = new GameObject($"pointeur de {nom}");
        pointeurSprite = pointeur.AddComponent<SpriteRenderer>();
        pointeurSprite.sprite = Resources.Load<Sprite>("Icons/settings_button");

        // Vérifiez si le sprite a bien été chargé
        if (pointeurSprite.sprite == null)
        {
            
        }

        pointeurSprite.enabled = false;
    }

    
    private void Start()
    {
        AnimaxActivate = false;
        ListDegat = new List<(Collision2D,Vector3)>();
        tag = "Animal";
        timeSpawn = Time.time;
        gameObject.layer = 6;
        currentInstance = new GameObject();
        StartCoroutine(Clignement());
    }
    

    protected void LoadData(string nom_animal)
    {
        animalData = Resources.Load<AnimalData>("Data/Animaux/" + nom_animal);
        pv = animalData.Pv;
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
        if (player == GameManager.Instance.joueur)
            currentInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Autre/joueur_equi"));
        else
            currentInstance = Instantiate(Resources.Load<GameObject>("Prefabs/Autre/joueur_adv"));
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
        destructibleSprite.Indestructible = true; 
        destructibleSprite.Optimize();
        destructibleSprite.Optimize();
        destructibleSprite.Optimize();
        destructibleSprite.RebuildAlphaTex();
        destructibleSprite.CropSprite = false;
        D2dPolygonCollider d2dPolygonCollider = gameObject.AddComponent<D2dPolygonCollider>();
        d2dPolygonCollider.Straighten = 0.01f;
        Rigidbody2D animalRigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        animalRigidbody2D.angularDrag = 3f;
        animalRigidbody2D.mass = 1000f;
        gameObject.AddComponent<DespawnManager>();
    }
    
    public abstract void Animax();

    public virtual void Degat(int damage)
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
        healthBar.SetHealth(pv);
    }

    public virtual void Soin(int heal)
    {
        pv += heal;
        if (animalData.Pv < pv)
            pv = animalData.Pv;
        healthBar.SetHealth(pv);
    }
    
  
    void OnCollisionEnter2D(Collision2D collison2D)
    {
        
        if (collison2D.gameObject.CompareTag("Explosion") && !ListDegat.Contains((collison2D,collison2D.gameObject.transform.position)))
        {
            D2dExplosion explosion = collison2D.gameObject.GetComponent<D2dExplosion>();
            Degat(explosion.degat);
            
            ListDegat.Add((collison2D,collison2D.gameObject.transform.position));
        }
        
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
                player.animaux_vivant.Enqueue(animal);
          
        }
        
        if (player.animalActif == this)
        {
            Instance.playerActif.enAction = false;
            Instance.tourActif = false;
            Destroy(this.currentInstance);
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
            screenPosition.y += 50; 
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
        while (currentInstance != null)
        {
            
            for (float t = 0; t <= 0.1f; t += Time.deltaTime / 5f)
            {
                if(currentInstance != null)
                    currentInstance.transform.position = new Vector3(currentInstance.transform.position.x, currentInstance.transform.position.y + t * 0.02f, currentInstance.transform.position.z);
                yield return null;
            }

          
            for (float t = 0; t <= 0.1f; t += Time.deltaTime / 5f)
            {
                if (currentInstance != null)
                 currentInstance.transform.position = new Vector3(currentInstance.transform.position.x, currentInstance.transform.position.y - t * 0.02f, currentInstance.transform.position.z);
                yield return null;
            }
        }
    }

    public void Meurt()
    {
        StartCoroutine(gameObject.GetComponent<DespawnManager>().Death());
    }
    
    protected IEnumerator Clignement()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3,7));
            
            spriteRenderer.sprite = animalData.Clignement;
            
            yield return new WaitForSeconds((float)0.2);
            
            spriteRenderer.sprite = animalData.sprite;
        }
    }
}
