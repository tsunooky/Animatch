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
    public class GameManager : MonoBehaviour
    { 
        public static GameManager Instance;
        public GameObject nextTurnButton; 
        public int tour;

        public PlayerManager joueur;

        public PlayerManager bot;

        private bool spawn = true;
        private bool animalBeingPlaced = false;

        public bool tourActif = false;

        public PlayerManager playerActif;
        
        public Text affichage_mana;

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
                        nextTurnButton.gameObject.SetActive(true);
                        drop_left.gameObject.SetActive(true);
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
                        nextTurnButton.gameObject.SetActive(false);
                        drop_left.gameObject.SetActive(false);
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
                                StartCoroutine(tirerbot(animalActif));
                            }
                            
                            
                        }
                    }

                    Debug.Log("le joueur qui vient de jouer n'a plus que " + playerActif.drops+ " drops !");
                    joueur.MettreAjourMain();
                    tour += 1;
                }
            }
        }

        [CanBeNull]
        public AnimalBehaviour getnearest()
        {
            if (joueur.animaux_vivant.Count > 0)
            {
                AnimalBehaviour res = joueur.animaux_vivant.Peek();
                var moi = this.transform.position;
                float minDistance = float.MaxValue;

                foreach (var animal in joueur.animaux_vivant)
                {
                    float distance = Vector2.Distance(moi, animal.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        res = animal;
                    }
                }
                Debug.Log(res.nom);
                return res;
            }

            return null;
        }

        IEnumerator tirerbot(AnimalBehaviour animalActif)
        {
            
            yield return new WaitForSeconds(2);
            var aimbotani = animalActif.AddComponent<AimBot>();
            var cible = getnearest();
            
            if (cible != null)
            {
                aimbotani.ClassiqueShootbot(cible.transform.position);
            }
            Destroy(aimbotani);
            FinfDuTour();
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
                // Pour le bot, attendre avant de placer l'animal
                yield return new WaitForSeconds(2f); // Attendre 2 secondes

                float randomX = UnityEngine.Random.Range(-6.5f, 7f);
                AnimalBehaviour newAnimal = creerAnimal(randomX, 4.7f, bot.deckAnimal.Dequeue());
                bot.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = bot;
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