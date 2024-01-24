using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool spawn = true;

    private AnimalData data;

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            // Vérifie si le joueur a cliqué et si le délai de spawn est écoulé
            if (Input.GetMouseButtonDown(0))
            {
                // Obtenez les coordonnées du clic de la souris
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Instanciez l'animal à la position du clic
                creer_animal(mousePosition.x, 10f);
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