using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;
using Script.Manager;

public class ProjKnifeBehaviour : ProjectileBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        GameObject cible = other.gameObject.transform.parent.gameObject;
        GameObject AnimalActif = GameManager.Instance.playerActif.animalActif.gameObject;
        bool invinsible = cible == AnimalActif; 
        Debug.Log( cible == AnimalActif);
        if (!invinsible)
        {
            if (!declanchement)
            {
                declanchement = true;
                var clone = Instantiate(Prefab, transform.position, transform.rotation);
                clone.SetActive(true);
                clone.GetComponent<D2dExplosion>().degat = projectileData.Degat;
                FinAction();
                Destroy(gameObject);
            }
        }
    }
}