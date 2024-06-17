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
        DefPassive += "Uppon death, bites in front of him.";
        projectile = Resources.Load<GameObject>("Prefabs/Projectile/Os 1");
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
    
    private IEnumerator CoupBat(Vector2 startPosition)
    {
        GameObject pivot = new GameObject("BatPivot");
        pivot.transform.position = startPosition;

        
        GameObject bat = Instantiate(projectile, pivot.transform.position, Quaternion.identity);
        bat.AddComponent<TouchToBump>();

        
        SpriteRenderer batSpriteRenderer = bat.GetComponent<SpriteRenderer>();
        Vector2 batSize = batSpriteRenderer.bounds.size;

        
        bat.transform.SetParent(pivot.transform);
        bat.transform.localPosition = new Vector3(0.5f, batSize.y / 2, 0); 

        
        float duration = 0.4f;
        float elapsedTime = 0.0f;

        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            
            float currentAngle = Mathf.Lerp(0, 360, t);

           
            pivot.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            yield return null;
        }

        
        Destroy(bat);
        Destroy(pivot);
        /*yield return StartCoroutine(gameObject.GetComponent<DespawnManager>().Death());
        CleanupOnDestroy();*/
    }
    
    private void OnDestroy()
    {
        //StartCoroutine(CoupBat(gameObject.transform.position));
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
            GameManager.Instance.playerActif.enAction = false;
            GameManager.Instance.tourActif = false;
            Destroy(this.currentInstance);
        }
    }

   /* private IEnumerator OnDestroyCoroutine()
    {
        yield return StartCoroutine(CoupBat(gameObject.transform.position));
        
        
    }

    private void CleanupOnDestroy()
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
            GameManager.Instance.playerActif.enAction = false;
            GameManager.Instance.tourActif = false;
            Destroy(this.currentInstance);
        }
    }

    public override void Meurt()
    {
        StartCoroutine(OnDestroyCoroutine());
        
        
    }*/
}
