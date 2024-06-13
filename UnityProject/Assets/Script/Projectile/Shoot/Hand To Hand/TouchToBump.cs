using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using UnityEngine.InputSystem;

public class TouchToBump : ProjectileBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject cible = other.gameObject.transform.parent.gameObject;
        GameObject AnimalActif = GameManager.Instance.playerActif.animalActif.gameObject;
        bool invinsible = cible == AnimalActif;
        if (!invinsible)
        {
            // Coordonnées de départ
            Vector2 startPosition = AnimalActif.transform.position;

            // Direction en fonction de l'angle de rotation
            Vector2 direction = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Vector2.right;

            // Coordonnées d'arrivée
            Vector2 endPosition = startPosition + direction;

            Vector2 bump = (endPosition - startPosition).normalized;
            cible.GetComponent<Rigidbody2D>().AddForce(bump * 50000);
            FinAction();
        }
    }
}

