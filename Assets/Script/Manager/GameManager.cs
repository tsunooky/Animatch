using System.Collections.Generic;
using System;
using UnityEngine;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    {

        static GameManager Instance;

        public int tour;

        public PlayerManager joueur;

        public PlayerManager bot;

        private bool spawn = true;
        
        //gameObject.AddComponent<AimAndShoot>();

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
                    spawn = false;
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
                        
                        // Lancer un rayon depuis la position de la souris dans la direction (0, 0, 1)
                        Collider2D collider = Physics2D.OverlapPoint(mousePosition);

                        // Vérifier s'il y a eu une collision
                        if (collider != null)
                        {
                            // Il y a eu une collision avec un collider
                            Debug.Log("Collision détectée avec : " + collider.name);
                        }
                        // Instanciez l'animal à la position du clic en x et y = hauteur
                        x.animaux_vivant.Add(creerAnimal(mousePosition.x, mousePosition.y, x.TemporaireEnAttendantProfil.Peek()));
                        x.TemporaireEnAttendantProfil.Pop();
                    }
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
    }
}