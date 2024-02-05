using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;
using Script.Manager;

public class ProjOsBehaviour : ProjectileBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!declanchement)
        {
            declanchement = true;
            GameManager.Instance.animalActif.gameObject.transform.position = gameObject.transform.position;
            Destroy(gameObject);
        }
    }
}
