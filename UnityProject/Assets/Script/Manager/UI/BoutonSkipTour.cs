using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Script.Manager;
using static Script.Manager.AGameManager;
using static CarteBehaviour;
public class BoutonSkipTour : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Gestion du passif du Panda
        AnimalBehaviour animalActif = GameManager.Instance.playerActif.animalActif;
        if (animalActif is PandaBehaviour)
        {
            // Passif du Panda
            animalActif.Soin(10);
        }
        
        Instance.FinDuTour();
    }
}
