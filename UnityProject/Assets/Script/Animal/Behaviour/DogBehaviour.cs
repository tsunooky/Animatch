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
        CoupBat(gameObject.transform.position);
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

        
        GameObject bat = Instantiate(potentielleprojDataAnimax.Projectile, pivot.transform.position, Quaternion.identity);
        bat.AddComponent<TouchToBump>();

        
        SpriteRenderer batSpriteRenderer = bat.GetComponent<SpriteRenderer>();
        Vector2 batSize = batSpriteRenderer.bounds.size;

        
        bat.transform.SetParent(pivot.transform);
        bat.transform.localPosition = new Vector3(0, batSize.y / 2, 0); 

        
        float duration = 0.2f;
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
        
    }
    
}
