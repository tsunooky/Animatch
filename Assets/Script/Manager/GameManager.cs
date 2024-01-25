using UnityEngine;

namespace Script.Manager
{
    public class GameManager : MonoBehaviour
    {
        private bool spawn = true;
        public float hauteurSpawn = 10f;

        static GameManager Instance;

        public int tour;

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
                    creer_animal(mousePosition.x, hauteurSpawn);
                    spawn = false;
                }
            }
        }
    
    
        // ReSharper disable Unity.PerformanceAnalysis
        void creer_animal(float x, float y)
        {
            // Création d'un GameObject
            GameObject newAnimal = new GameObject("Animal"+x);
            TortueBehaviour animalBehaviour = newAnimal.AddComponent<TortueBehaviour>();
            newAnimal.AddComponent<AimAndShoot>();
            newAnimal.transform.position = new Vector2(x, y);
            animalBehaviour.AnimalVisible();
        }
    
    }
}