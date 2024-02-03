using System.Collections.Generic;
using System;
using UnityEngine;

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
        
        private Dictionary<string, Type> animalTypes = new Dictionary<string, Type>
        {
            { "turtle", typeof(TurtleBehaviour) },
            { "panda", typeof(PandaBehaviour) },
            { "dog", typeof(DogBehaviour) }
        };

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GameManager n'est plus un singleton car il viens d'être redéfinis une deuxième fois !");
                return;
            }
            
            
            Instance = this;
            joueur =  gameObject.AddComponent<PlayerManager>();
            bot = gameObject.AddComponent<PlayerManager>();
        }
    
        void Update()
        {
            if (spawn)
            {
                if (joueur.TemporaireEnAttendantProfil.Count == 0 && bot.TemporaireEnAttendantProfil.Count == 0)
                {
                    spawn = false;
                }
                else
                {
                    PlayerManager x;
                    if (joueur.TemporaireEnAttendantProfil.Count > bot.TemporaireEnAttendantProfil.Count)
                        x = joueur;
                    else
                        x = bot;
                    // Vérifie si le joueur a cliqué
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Obtenez les coordonnées du clic de la souris
                        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        // Instanciez l'animal à la position du clic en x et y = hauteur
                        AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y,
                            x.TemporaireEnAttendantProfil.Peek());
                        x.animaux_vivant.Enqueue(newAnimal);
                        newAnimal.player = x;
                        x.TemporaireEnAttendantProfil.Pop();
                        
                    }
                }
            }
            else
            {
                if (!tourActif)
                {
                    if (tour % 2 == 0)
                    {
                        Debug.Log("C'est votre tour");
                        bot.tourActif = false;
                        joueur.tourActif = true;
                        if (joueur.animaux_vivant.Count == 0)
                        {
                            Win(bot);
                        }
                        else
                        {
                            AnimalBehaviour animalActif = joueur.animaux_vivant.Dequeue();
                            joueur.animaux_vivant.Enqueue(animalActif);
                            joueur.animalActif = animalActif;
                        }
                    }
                    else
                    {
                        joueur.tourActif = false;
                        bot.tourActif = true;
                        if (bot.animaux_vivant.Count == 0)
                        {
                            Win(joueur);
                        }
                        else
                        {
                            AnimalBehaviour animalActif = bot.animaux_vivant.Dequeue();
                            bot.animaux_vivant.Enqueue(animalActif);
                            bot.animalActif = animalActif;
                            AimAndShoot animalActifBot = animalActif.gameObject.AddComponent<AimAndShoot>();
                            animalActifBot.Initialize("tomate");
                            animalActifBot.ShootBOT(Vector3.up);
                            Debug.Log("Tour du bot");
                        }
                    }

                    tourActif = true;
                    tour += 1;
                }
            }
        }
    
    
        // ReSharper disable Unity.PerformanceAnalysis
        AnimalBehaviour creerAnimal(float x, float y,string animal)
        {
            if (animalTypes.ContainsKey(animal))
            {
                // Création d'un GameObject
                GameObject newAnimal = new GameObject(animal + x);
                Type typeAnimal = animalTypes[animal];
                AnimalBehaviour animalBehaviour = (AnimalBehaviour)(newAnimal.AddComponent(typeAnimal));
                newAnimal.transform.position = new Vector2(x, y);
                animalBehaviour.AnimalVisible();
                return animalBehaviour;
            }
            throw new Exception("Ce type d'animal n'existe pas ");
        }

        private void Win(PlayerManager player)
        {
            Debug.Log("Victoire de " + player.name);
            Application.Quit();
        }
    }
}