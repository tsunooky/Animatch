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
    public abstract class AGameManager : MonoBehaviour
    {
        public PlayerManager joueur;
        public static AGameManager Instance;
        public PlayerManager playerActif;
        public bool tourActif;
        public Text affichage_mana;
        #region Code Moved
        protected AnimalBehaviour creerAnimal(float x, float y,string animal)
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
        
        public void FinDuTour()
        {
            // regle le bug #01
            tourActif = false;
            Destroy(playerActif.animalActif.currentInstance);
        }
        
        protected void Win(PlayerManager player)
        {
            Debug.Log("Victoire de " + player.name);
            SceneManager.LoadScene("Fin");
            Invoke("QuitGame", 10f);
        }
        
        protected void QuitGame()
        {
            Application.Quit();
        }
        #endregion
    }
}