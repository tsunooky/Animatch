using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class TouchToBump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject cible = other.gameObject.transform.parent.gameObject;
        GameObject AnimalActif = GameManager.Instance.playerActif.animalActif.gameObject;
        bool invinsible = cible == AnimalActif;
        if (!invinsible)
        {
            
            Vector2 startPosition = AnimalActif.transform.position;

           
            Vector2 ciblePosition = cible.transform.position;
            
            Vector2 bumpDirection = (ciblePosition - startPosition);

            
            float bumpForce = 150000; 
            cible.GetComponent<Rigidbody2D>().AddForce(bumpDirection * bumpForce);
        }
    }
}

