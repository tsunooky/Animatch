using System;
using UnityEngine;
using Script.Manager;

public class ProjBambooBehaviour: ProjectileBehaviour
{
    private GameObject Bamboo;

    private void Awake()
    {
        Bamboo = Resources.Load<GameObject>("Prefabs/Autre/Bamboo");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject cible = other.gameObject.transform.parent.gameObject;
        GameObject AnimalActif = GameManager.Instance.playerActif.animalActif.gameObject;
        bool invinsible = cible == AnimalActif; 
        if (!declanchement && !invinsible)
        {
            declanchement = true;
            Debug.Log("aaaaaaaaaaaaaa");
            Instantiate(Bamboo, gameObject.transform);
            FinAction();
            Destroy(gameObject);
        }
    }

}