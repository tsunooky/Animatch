using System.Collections.Generic;
using System;
using System.Collections;
using Script.Data;
using UnityEngine;
using Script.Manager;
using UnityEngine.UI;
using Photon.Pun;
using UnityEditor;
using UnityEngine.SceneManagement;
using static Unity.Mathematics.Random;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    { 
        public static GameManager Instance;

        public int tour;

        public PlayerManager joueur;

        public PlayerManager bot;

        private bool spawn = true;
        private bool animalBeingPlaced = false;

        public bool tourActif = false;

        public PlayerManager playerActif;
        public Text affichage_mana;
        
      
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GameManager n'est plus un singleton car il viens d'être redéfinis une deuxième fois !");
                return;
            }
            
            
            Instance = this;
            joueur =  gameObject.AddComponent<PlayerManager>();
            joueur.CreateProfil();
            joueur.CreerMain();
            bot = gameObject.AddComponent<PlayerManager>();
            bot.CreateProfil();
            
            
            // Evité bug au lancement
            playerActif = joueur;
            tour = 1;
        }
    
        void Update()
        {
            
            if (spawn)
            {
                if (joueur.deckAnimal.Count == 0 && bot.deckAnimal.Count == 0)
                {
                    spawn = false;
                }
                else
                {
                    
                    if (!animalBeingPlaced)
                    {
                        if (joueur.deckAnimal.Count > bot.deckAnimal.Count)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                StartCoroutine(PlaceAnimal(joueur));
                            }
                        }
                        else
                        {
                            StartCoroutine(PlaceAnimal(bot));
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
                        if (joueur.animaux_vivant.Count == 0)
                        {
                            Win(bot);
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
                        playerActif = bot;
                        if (bot.animaux_vivant.Count == 0)
                        {
                            Win(joueur);
                        }
                        else
                        {
                            affichage_mana.text = $" ? ";
                            Debug.Log("Tour du bot");
                            AnimalBehaviour animalActif = bot.animaux_vivant.Dequeue();
                            animalActif.LoadAura();
                            bot.animaux_vivant.Enqueue(animalActif);
                            playerActif.animalActif = animalActif;
                            if (animalActif!=null)
                            {
                                AimAndShoot animalActifBot = animalActif.gameObject.AddComponent<AimAndShoot>();
                                animalActifBot.Initialize("tomate");
                                animalActifBot.Shoot(Vector3.up);
                            }
                            
                            FinfDuTour();
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
                AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, player.deckAnimal.Dequeue());
                player.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = player;
                newAnimal.LoadHealthbar();
            }
            else
            {
                // Pour le bot, attendre avant de placer l'animal
                yield return new WaitForSeconds(2f); // Attendre 2 secondes

                float randomX = UnityEngine.Random.Range(-6.5f, 7f);
                AnimalBehaviour newAnimal = creerAnimal(randomX, 4.7f, player.deckAnimal.Dequeue());
                player.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = player;
                newAnimal.LoadHealthbar();
            }

            animalBeingPlaced = false;
        }
    
    
        // ReSharper disable Unity.PerformanceAnalysis
        AnimalBehaviour creerAnimal(float x, float y,string animal)
        {
            var animalTypes = DataDico.animalTypes;
            if (animalTypes.ContainsKey(animal))
            {
                // Création d'un GameObject
                GameObject newAnimal = new GameObject(animal + x);
                Type typeAnimal = animalTypes[animal];
                AnimalBehaviour animalBehaviour = (AnimalBehaviour)(newAnimal.AddComponent(typeAnimal));
                newAnimal.transform.position = new Vector2(x, y);
                animalBehaviour.AnimalVisible();
                animalBehaviour.nom = animal + x;
                animalBehaviour.setPointeur();
                return animalBehaviour;
            }
            throw new Exception("Ce type d'animal n'existe pas ");
        }

        public void FinfDuTour()
        {
            // regle le bug #01
            tourActif = false;
            Destroy(playerActif.animalActif.currentInstance);
            playerActif.animalActif.currentInstance = new GameObject();
        }

        private void Win(PlayerManager player)
        {
            Debug.Log("Victoire de " + player.name);
            SceneManager.LoadScene("Fin");
            Invoke("QuitGame", 10f);
        }
        private void QuitGame()
        {
            Application.Quit();
        }
    }

    
    }
