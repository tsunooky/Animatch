using System.Collections.Generic;
using System;
using System.Collections;
using JetBrains.Annotations;
using Script.Data;
using UnityEngine;
using Script.Manager;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;
using static Unity.Mathematics.Random;
using static Unity.Mathematics.math;



namespace Script.Manager

{
    public abstract class AGameManager : MonoBehaviourPunCallbacks
    {
        public PlayerManager? joueur;
        public int tour;
        protected bool spawn = true;
        public static AGameManager Instance;
        public PlayerManager playerActif;
        public bool tourActif;
        public Text affichage_mana;
        
        public GameObject animax;
        public GameObject passif;
        
        #region Code Moved
        
        
        protected AnimalBehaviour creerAnimal(float x, float y,string animal,PlayerManager player)
        {
            var animalTypes = DataDico.animalTypes;
            if (animalTypes.ContainsKey(animal))
            {
                // Création d'un GameObject
                GameObject newAnimal = new GameObject(animal + x);
                Type typeAnimal = animalTypes[animal];
                AnimalBehaviour animalBehaviour = (AnimalBehaviour)(newAnimal.AddComponent(typeAnimal));
                animalBehaviour.player = player;
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

        public void Win(PlayerManager player, bool res)
        {
            gameObject.AddComponent<WinGame>().Win(player,res);
            
        }

        protected void QuitGame()
        {
            Application.Quit();
        }
        #endregion
    }
}