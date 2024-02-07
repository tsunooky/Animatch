using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;

public class BoutonAnimax : MonoBehaviour
{
    private void OnMouseDown()
    {
        AnimalBehaviour animalActif = GameManager.Instance.animalActif;
        Debug.Log(animalActif != null);
        if (GameManager.Instance.joueur.tourActif)
        {
            animalActif.Animax();
        }
    }
}
