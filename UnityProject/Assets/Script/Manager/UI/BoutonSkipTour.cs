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
        
        AnimalBehaviour animalActif = Instance.playerActif.animalActif;
        if (animalActif is PandaBehaviour)
        {
            animalActif.Soin(10);
        }
        
        Instance.FinDuTour();
    }
}
