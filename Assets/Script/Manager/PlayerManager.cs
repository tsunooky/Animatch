using System.Collections;
using System.Collections.Generic;
using Script.Carte;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Public Profil profil;

    public List<AnimalBehaviour> animaux_vivant;
    public Queue<CarteBehaviour> main;
    public int drops;
    public Dictionary<string, bool> passif_actifs;

    public void TuerAnimal(AnimalBehaviour animalBehaviour)
    {
        Destroy(animalBehaviour.gameObject);
    }

    /*public void RessuciterAnimal(AnimalBehaviour animalBehaviour)
    {
       mécanique utlisant le deck d'animaux de Profil non implémenter pour le moment 
    }*/

    public void PiocherMain()
    {
        /* 
            mécanique utlisant le deck de carte de Profil non implémenter pour le moment 
            main.Enqueue(carte);
        */
    }

    public void DefausserMain()
    {
        main.Dequeue();
    }

    public void MiseAJourDrops(int tour)
    {
        /*
         * A voir
         * 
         */
    }
}
