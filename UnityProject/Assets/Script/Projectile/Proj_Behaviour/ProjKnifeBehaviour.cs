using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;
using Script.Manager;
using Unity.VisualScripting;

public class ProjKnifeBehaviour : ProjectileBehaviour
{
    private void Awake()
    {
        // Retourne le couteau car le sprite est a l'envers tar adrien
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.flipY = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent is not null)
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
                    
                    Destroy(gameObject);
                }
            }
        }
    }
}