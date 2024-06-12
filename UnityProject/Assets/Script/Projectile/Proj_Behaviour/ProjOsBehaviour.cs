using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;
using Script.Manager;

public class ProjOsBehaviour : ProjectileBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject cible = other.gameObject.transform.parent.gameObject;
        GameObject AnimalActif = GameManager.Instance.playerActif.animalActif.gameObject;
        bool invinsible = cible == AnimalActif; 
        if (!declanchement && !invinsible)
        {
            declanchement = true;
            GameManager.Instance.playerActif.animalActif.gameObject.transform.position = gameObject.transform.position;
            FinAction();
            Destroy(gameObject);
        }
    }
}
