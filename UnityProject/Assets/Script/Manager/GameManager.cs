using System.Collections.Generic;
using System;
using Script.Data;
using UnityEngine;
using Script.Manager;
using UnityEngine.UI;
using Photon.Pun;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    { 
        public static GameManager Instance;

        public int tour;

        public PlayerManager joueur;

        public PlayerManager bot;

        private bool spawn = true;

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
                    PlayerManager x;
                    if (joueur.deckAnimal.Count > bot.deckAnimal.Count)
                    {
                        x = joueur;
                    }
                    else
                        x = bot;
                    // Vérifie si le joueur a cliqué
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Obtenez les coordonnées du clic de la souris
                        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        // Instanciez l'animal à la position du clic en x et y = hauteur
                        AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y,
                            x.deckAnimal.Dequeue());
                        x.animaux_vivant.Enqueue(newAnimal);
                        newAnimal.player = x;
						newAnimal.LoadHealthbar();
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
