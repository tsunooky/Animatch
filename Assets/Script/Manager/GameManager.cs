using System.Collections.Generic;
using System;
using UnityEngine;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    {
        private bool spawn = true;
        public float hauteurSpawn = 10f;

        static GameManager Instance;

        public int tour;

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
        }
    
        void Update()
        {
            if (spawn)
            {
                // Vérifie si le joueur a cliqué
                if (Input.GetMouseButtonDown(0))
                {
                    // Obtenez les coordonnées du clic de la souris
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    // Instanciez l'animal à la position du clic en x et y = hauteur
                    creer_animal(mousePosition.x, hauteurSpawn,"turtle");
                    spawn = false;
                }
            }
        }
    
    
        // ReSharper disable Unity.PerformanceAnalysis
        void creer_animal(float x, float y,string animal)
        {
            if (animalTypes.ContainsKey(animal))
            {
                // Création d'un GameObject
                GameObject newAnimal = new GameObject(animal + x);
                Type typeAnimal = animalTypes[animal];
                AnimalBehaviour animalBehaviour = (AnimalBehaviour)(newAnimal.AddComponent(typeAnimal));
                animalBehaviour.LancerPouvoir();
                newAnimal.transform.position = new Vector2(x, y);
                animalBehaviour.AnimalVisible();
            }
            else
            {
                Debug.Log("erreur ce type d'animal n'existe pas encore dans le jeu");
            }
        }
    
    }
}