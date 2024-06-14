using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class TouchToBump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Joss est vraiment un GIGA GOAT");
        GameObject cible = other.gameObject.transform.parent.gameObject;
        GameObject AnimalActif = GameManager.Instance.playerActif.animalActif.gameObject;
        bool invinsible = cible == AnimalActif;
        if (!invinsible)
        {
            // Coordonnées de départ (animal actif)
            Vector2 startPosition = AnimalActif.transform.position;

            // Coordonnées de la cible
            Vector2 ciblePosition = cible.transform.position;

            // Direction de rejet (de la cible vers l'animal actif, inversée)
            Vector2 bumpDirection = (ciblePosition - startPosition).normalized * -1;

            // Appliquer la force de rejet à la cible
            float bumpForce = 50; // Ajuster cette valeur si nécessaire
            cible.GetComponent<Rigidbody2D>().AddForce(bumpDirection * bumpForce);
        }
    }
}

