using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;

public class ProjTomateBehaviour : ProjectileBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!declanchement)
        { 
            declanchement = true;
            var clone = Instantiate(Prefab, transform.position, transform.rotation);
            clone.SetActive(true);
            clone.GetComponent<D2dExplosion>().degat = projectileData.Degat;
            FinAction();
            Destroy(gameObject);
        }
    }
    
}