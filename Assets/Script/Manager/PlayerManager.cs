using System;
using System.Collections;
using System.Collections.Generic;
using Script.Data;
using Script.Manager;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Public Profil profil;

    public Queue<string> deckAnimal = new Queue<string>();
    public Queue<AnimalBehaviour> animaux_vivant;
    public Queue<Type> main = new Queue<Type>();
    public int drops;
    public Dictionary<string, bool> passif_actifs;
    public bool tourActif = false;
    public AnimalBehaviour animalActif;
    public ProfilManager profil = new ProfilManager("Joss","LePlusFort");


    public void CreateProfil()
    {
        animaux_vivant = new Queue<AnimalBehaviour>();
        trierAnimaux();
        drops = 1;
        //DEFINITION DE TOUT LES PASSIFS
        passif_actifs = new Dictionary<string, bool>();
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

    public void trierAnimaux()
    {
        if (profil.deckAnimaux.Length != 3)
            throw new ArgumentException("BUG DETECTER DECK INCOMPLET");
        foreach (string animal in profil.deckAnimaux)
        {
            deckAnimal.Enqueue(animal);
        }
    }

    public void CreerMain()
    {
        for (int i = 0; i < 4; i++)
        {
            main.Enqueue(DataDico.carteTypes[profil.deckCartes[i]]);
        }
    }
}
