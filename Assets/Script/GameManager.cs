using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool spawn = true;
    void Start()
    {
    }

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
                creer_animal(mousePosition.x,10f);
                spawn = false;
            }
        }
    }

    void creer_animal(float x, float y)
    {
        // Création d'un GameObject
        GameObject Animal1 = new GameObject("Animal1");
        Animal1.AddComponent<AnimalController>();
        Animal1.transform.position = new Vector2(x, y);;
    }
    
}