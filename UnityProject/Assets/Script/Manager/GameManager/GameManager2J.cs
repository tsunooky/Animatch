using System.Collections.Generic;
using System;
using System.Collections;
using JetBrains.Annotations;
using Script.Data;
using UnityEngine;
using Script.Manager;
using UnityEngine.UI;
using Photon.Pun;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;
using static Unity.Mathematics.Random;
using static Unity.Mathematics.math;
namespace Script.Manager
{
    public class GameManager2J : AGameManager
    {
        public GameObject nextTurnButton; 
        public PlayerManager joueur2;
        private bool animalBeingPlaced = false;
        public GameObject drop_left;
      
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GameManager n'est plus un singleton car il viens d'être redéfinis une deuxième fois !");
                return;
            }

            drop_left.gameObject.SetActive(false);
            nextTurnButton.gameObject.SetActive(false);
            Instance = this;
            joueur =  gameObject.AddComponent<PlayerManager>();
            playerActif = joueur;
            joueur.CreateProfil();
            joueur.CreerMain();
            joueur.mainManager = GameObject.FindGameObjectsWithTag("MainCarte");
            joueur2 = gameObject.AddComponent<PlayerManager>();
            playerActif = joueur2;
            joueur2.CreateProfil();
            joueur2.CreerMain();
            joueur2.mainManager = GameObject.FindGameObjectsWithTag("MainCarte2J");
            UpdateCardDisplay(joueur);
            int i = 1;
            foreach (var card in joueur.mainManager)  
            {
                    Debug.Log($"La carte {i} appartient au joueur 1");
                    i++;
            }
            foreach (var card in joueur2.mainManager)  
            {
                Debug.Log($"La carte {i} appartient au joueur 2");
                i++;
            }
            
            // Evité bug au lancement
            playerActif = joueur;
            tour = 1;
        }
    
        void Update()
        {
            if (spawn)
            {
                if (joueur.deckAnimal.Count == 0 && joueur2.deckAnimal.Count == 0)
                {
                    spawn = false;
                }
                else
                {
                    if (!animalBeingPlaced)
                    {
                        if (joueur.deckAnimal.Count > joueur2.deckAnimal.Count)
                        {
                            if (Input.GetMouseButtonDown(0))
                                StartCoroutine(PlaceAnimal(joueur));
                        }
                        else
                        {
                            if(Input.GetMouseButtonDown(0))
                                StartCoroutine(PlaceAnimal(joueur2));
                        }
                    }
                }
            }
            else
            {
                if (!tourActif)
                {
                    tourActif = true;
                    if (tour % 2 != 0)
                    {
                        Debug.Log("C'est votre tour");
                        playerActif = joueur;
                        joueur.MiseAJourDrops(tour);
                        affichage_mana.text = $"{joueur.drops}";
                        nextTurnButton.gameObject.SetActive(true);
                        drop_left.gameObject.SetActive(true);
                        UpdateCardDisplay(playerActif);
                        if (joueur.animaux_vivant.Count == 0)
                        {
                            Win(joueur2);
                        }
                        else
                        {
                            AnimalBehaviour animalActif = joueur.animaux_vivant.Dequeue();
                            joueur.animaux_vivant.Enqueue(animalActif);
                            playerActif.animalActif = animalActif;
                            animalActif.LoadAura();
                            joueur.MiseAjourAffichageDrops();
                        }
                    }
                    else
                    {
                        Debug.Log("C'est Le Tour du Joueur 2");
                        playerActif = joueur2;
                        joueur2.MiseAJourDrops(tour);
                        affichage_mana.text = $"{joueur2.drops}";
                        UpdateCardDisplay(playerActif);
                        if (joueur2.animaux_vivant.Count == 0)
                        {
                            Win(joueur);
                        }
                        else
                        {
                            AnimalBehaviour animalActif = joueur2.animaux_vivant.Dequeue();
                            joueur2.animaux_vivant.Enqueue(animalActif);
                            playerActif.animalActif = animalActif;
                            animalActif.LoadAura();
                            joueur2.MiseAjourAffichageDrops();
                        }
                    }

                    Debug.Log("le joueur qui vient de jouer n'a plus que " + playerActif.drops+ " drops !");
                    joueur.MettreAjourMain();
                    tour += 1;
                }
            }
        }
        
        IEnumerator PlaceAnimal(PlayerManager player)
        {
            animalBeingPlaced = true;

            if (player == joueur)
            {
                
                // Pour le joueur, attendre un clic de souris
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null;
                }

                // Obtenez les coordonnées du clic de la souris
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Instanciez l'animal à la position du clic en x et y = hauteur
                AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, joueur.deckAnimal.Dequeue());
                joueur.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = joueur;
                newAnimal.LoadHealthbar();
            }
            else
            {
                // Pour le joueur, attendre un clic de souris
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null;
                }

                // Obtenez les coordonnées du clic de la souris
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Instanciez l'animal à la position du clic en x et y = hauteur
                AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, joueur2.deckAnimal.Dequeue());
                joueur2.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = joueur2;
                newAnimal.LoadHealthbar();
            }

            player.animalActif = player.animaux_vivant.Peek();

            animalBeingPlaced = false;
        }
        public void UpdateCardDisplay(PlayerManager currentPlayer)
        {
            //Le joueur donné est le joueur qui joue
            if (currentPlayer == joueur)
            {
                foreach(var card in joueur.mainManager)
                {
                    card.SetActive(true);
                }
                foreach(var card2 in joueur2.mainManager)
                {
                    card2.SetActive(false);
                }    
            }
            else
            {
                foreach(var card3 in joueur.mainManager)
                {
                    card3.SetActive(false);
                }
                foreach(var card4 in joueur2.mainManager)
                {
                    card4.SetActive(true);
                }
            }
            

            
        }   
    }
}

