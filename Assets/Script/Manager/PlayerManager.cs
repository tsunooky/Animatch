using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Public Profil profil;

    public Queue<AnimalBehaviour> animaux_vivant;
    public Queue<CarteBehaviour> main;
    public int drops;
    public Dictionary<string, bool> passif_actifs;
    public Stack<string> TemporaireEnAttendantProfil;
    public bool tourActif = false;
    public AnimalBehaviour animalActif;


    public PlayerManager()
    {
        animaux_vivant = new Queue<AnimalBehaviour>();
        drops = 1;
        //DEFINITION DE TOUT LES PASSIFS
        passif_actifs = new Dictionary<string, bool>();
        TemporaireEnAttendantProfil = new Stack<string>();
        TemporaireEnAttendantProfil.Push("turtle");
        TemporaireEnAttendantProfil.Push("panda");
        TemporaireEnAttendantProfil.Push("dog");
    }
    
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
