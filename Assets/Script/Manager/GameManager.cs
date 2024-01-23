using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int spawn = 6;

    private AnimalData data;

    // Update is called once per frame
    void Update()
    {
        if (spawn > 0)
        {
            // Vérifie si le joueur a cliqué et si le délai de spawn est écoulé
            if (Input.GetMouseButtonDown(0))
            {
                // Obtenez les coordonnées du clic de la souris
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Instanciez l'animal à la position du clic
                spawn = creer_animal(spawn, mousePosition.x, 10f);
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    int creer_animal(int spawn ,float x, float y)
    {
        // Création d'un GameObject
        GameObject newAnimal = new GameObject("Animal"+x);
        TortueBehaviour animalBehaviour = newAnimal.AddComponent<TortueBehaviour>();
        newAnimal.AddComponent<AimAndShoot>();
        newAnimal.transform.position = new Vector2(x, y);
        animalBehaviour.AnimalVisible();
        return spawn - 1;
    }
    
}