using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D.Examples;
using Script.Manager;
using Unity.Mathematics;

public class ProjBombeBehaviour : ProjectileBehaviour
{
    private GameObject Atomique;

    private void Awake()
    {
        Atomique = Resources.Load<GameObject>("Prefabs/Projectile/bomb_p 1");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.parent is not null)
        {
            GameObject cible = other.gameObject.transform.parent.gameObject;
            GameObject AnimalActif = AGameManager.Instance.playerActif.animalActif.gameObject;
            bool invinsible = cible == AnimalActif;
            if (!invinsible)
            {
                if (!declanchement)
                {
                    declanchement = true;
                    var startPosition = gameObject.transform.position;
                    startPosition.y += 8;
                    GameObject atomique = Instantiate(Atomique, startPosition, Quaternion.Inverse(quaternion.identity));
                    atomique.GetComponent<ProjectileBehaviour>().Prefab = projectileData.Explosion;
                    atomique.GetComponent<ProjectileBehaviour>().projectileData = projectileData;
                    Destroy(gameObject);
                }
            }
        }
    }
}