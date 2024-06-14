using System;
using System.Collections;
using System.Collections.Generic;
using Script.Data;
using Script.Manager;
using UnityEngine;
using System.Linq;
using static Script.Manager.AGameManager;


public class PlayerManager : MonoBehaviour
{
    //Public Profil profil;

    public Queue<string> deckAnimal = new Queue<string>();
    public Queue<AnimalBehaviour> animaux_vivant;
    public Queue<Type> main = new Queue<Type>();
    public int drops;
    public Dictionary<string, bool> passif_actifs;
    public ProfilManager profil = new ProfilManager();
    public GameObject[] mainManager;
    public AnimalBehaviour animalActif;
    public bool enAction;
    public bool enVisee;
    public bool IsBot = false;
    public bool IsPlayer2 = false;

    public void Awake()
    {
        enAction = false;
        animalActif = null;
        if (Instance is GameManager2J && this != Instance.joueur)
            mainManager = GameObject.FindGameObjectsWithTag("MainCarte2J");
        else
            mainManager = GameObject.FindGameObjectsWithTag("MainCarte");
        enVisee = false;
        animalActif = null;
    }

    public void CreateProfil()
    {
        animaux_vivant = new Queue<AnimalBehaviour>();
        trierAnimaux();
        drops = 1;
        passif_actifs = new Dictionary<string, bool>();
    }
    
    public void TuerAnimal(AnimalBehaviour animalBehaviour)
    {
        Destroy(animalBehaviour.gameObject);
    }
    
    
    
    public void DefausserMain()
    {
        
        main.Dequeue();
        MettreAjourMain();
    }

    public void MiseAJourDrops(int tour)
    {
        if (animaux_vivant.Peek() is RatBehaviour)
            drops = (tour / 2 + tour % 2) + 1;
        else
            drops = tour / 2 + tour % 2;
        if (drops > 6)
            drops = 6;
    }

    public void trierAnimaux()
    {
        if (profil.deckAnimaux.Length != 3)
            throw new ArgumentException("DECK INCOMPLET");
        if (IsBot)
        {
            foreach (string animal in profil.deckAnimauxbot)
            {
                deckAnimal.Enqueue(animal);
            } 
        }
        else if (IsPlayer2)
        {
            foreach (string animal in profil.deckAnimauxPlayer2)
            {
                deckAnimal.Enqueue(animal);
            }
        }
        else
        {
            foreach (string animal in profil.deckAnimaux)
            {
                deckAnimal.Enqueue(animal);
            }
        }
        
    }

    public void CreerMain()
    {
        
        // Méthode pour piocher la main initiale
        for (int i = 0; i < 8; i++)
        {
            main.Enqueue(DataDico.carteTypes[profil.deckCartes[i]]);
        }

        foreach (var card in mainManager)
        {
            var carteBehaviour = card.GetComponent<CarteBehaviour>();
            if (carteBehaviour != null)
            {
                carteBehaviour.Initialize(this);
            }
        }
        MettreAjourMain();
    }
    
    public void MettreAjourMain()
    {
        foreach (GameObject carte in mainManager)
        {
            var typeCarte = main.Dequeue();
            var a = carte.GetComponent(typeCarte);
            if (a == null)
            {
                carte.AddComponent(typeCarte);
            }
            main.Enqueue(typeCarte);
        }
    }
    
    public void SetAura(bool aura){}
    public void MiseAjourAffichageDrops()
    {
        //Mise a jour de l'affichage des drops
        GameManager.Instance.affichage_mana.text = $"{drops}";
    }
}
