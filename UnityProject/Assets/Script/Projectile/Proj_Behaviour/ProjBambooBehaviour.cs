using System;
using Destructible2D;
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
        if (other.gameObject.transform.parent is not null)
        {
            GameObject cible = other.gameObject.transform.parent.gameObject;
            GameObject AnimalActif = GameManager.Instance.playerActif.animalActif.gameObject;
            bool invinsible = cible == AnimalActif;
            if (!declanchement && !invinsible)
            {
                declanchement = true;
                Debug.Log("ADDDRIEN");
                Instantiate(Bamboo, gameObject.transform.position, gameObject.transform.rotation);
                FinAction();
                Destroy(gameObject);
            }
        }
    }

}