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
            other.gameObject.transform.parent.gameObject.transform.position = new Vector3();
            FinAction();
        }
    }
}
