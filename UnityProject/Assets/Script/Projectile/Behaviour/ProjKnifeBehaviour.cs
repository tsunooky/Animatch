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
        if (!invinsible)
        {
            if (!declanchement)
            {
                if (cible.CompareTag("Animal"))
                {
                    AnimalBehaviour animal = cible.GetComponent<AnimalBehaviour>();
                    animal.Degat(projectileData.Degat);
                }
                FinAction();
                Destroy(gameObject);
            }
        }
    }
}