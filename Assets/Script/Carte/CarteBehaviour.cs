using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarteBehaviour : MonoBehaviour
{
    // Créer un événement UnityEvent pour le clic
    public UnityEvent onClickEvent;

    private void Start()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
    }

    private void OnMouseDown()
    {
        // Exécuter toutes les fonctions abonnées à l'événement onClickEvent
        onClickEvent.Invoke();
    }
}
