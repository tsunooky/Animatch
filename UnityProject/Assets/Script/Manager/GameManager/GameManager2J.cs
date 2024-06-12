using System.Collections.Generic;
using System;
using Script.Data;
using UnityEngine;
using Script.Manager;
using UnityEngine.UI;
using Photon.Pun;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace Script.Manager
{
    public class GameManager2J : MonoBehaviour
    {
        public static GameManager2J Instance;

        public int tour;

        public PlayerManager joueur;

        public PlayerManager joueur2;

        private bool spawn = true;

        public bool tourActif = false;

        public PlayerManager playerActif;
        public Text affichage_mana;
        public Text Tour;
        private bool isPlayerTurn = true;
        private bool isProcessingTurn = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GameManager n'est plus un singleton car il viens d'être redéfinis une deuxième fois !");
                return;
            }


            Instance = this;
            joueur = gameObject.AddComponent<PlayerManager>();
            joueur.CreateProfil();
            joueur.CreerMain();
            joueur2 = gameObject.AddComponent<PlayerManager>();
            joueur2.CreateProfil();
            joueur2.CreerMain();



            // Evité bug au lancement
            playerActif = joueur;
            tour = 1;
        }

        void Update()
        {
            if (spawn)
            {
                HandleSpawning();
            }
            else
            {
                if (!isProcessingTurn)
                {
                    if (isPlayerTurn)
                    {
                        StartCoroutine(GererTourJoueur1());
                    }
                    else
                    {
                        StartCoroutine(GererTourJoueur2());
                    }
                }
            }
        }
        private void HandleSpawning()
        {
            if (joueur.deckAnimal.Count == 0 && joueur2.deckAnimal.Count == 0)
            {
                spawn = false;
            }
            else
            {
                PlayerManager x = (joueur.deckAnimal.Count > joueur2.deckAnimal.Count) ? joueur : joueur2;

                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, x.deckAnimal.Dequeue());
                    x.animaux_vivant.Enqueue(newAnimal);
                    newAnimal.player = x;
                    newAnimal.LoadHealthbar();
                }
            }
        }
        
        #region GererJoueur
        
        private System.Collections.IEnumerator GererTourJoueur1()
        {
            isProcessingTurn = true;
            playerActif = joueur;
            Tour.text = "C'est le tour du Joueur 1";
            Tour.enabled = true;

            // Attendre 3 secondes
            yield return new WaitForSeconds(3);

            if (joueur.animaux_vivant.Count == 0)
            {
                Win(joueur2);
            }
            else
            {
                AnimalBehaviour animalActif = joueur.animaux_vivant.Dequeue();
                joueur.animaux_vivant.Enqueue(animalActif);
                playerActif.animalActif = animalActif;
                animalActif.LoadAura();
                joueur.MiseAjourAffichageDrops();
            }
            isProcessingTurn = false;
            isPlayerTurn = false;
        }
        private System.Collections.IEnumerator GererTourJoueur2()
        {
            isProcessingTurn = true;
            playerActif = joueur2;
            Tour.text = "C'est le tour du Joueur 2";
            Tour.enabled = true;

            // Attendre 3 secondes
            yield return new WaitForSeconds(3);

            if (joueur2.animaux_vivant.Count == 0)
            {
                Win(joueur);
            }
            else
            {
                AnimalBehaviour animalActif = joueur2.animaux_vivant.Dequeue();
                joueur2.animaux_vivant.Enqueue(animalActif);
                playerActif.animalActif = animalActif;
                animalActif.LoadAura();
                joueur2.MiseAjourAffichageDrops();
                FinfDuTour();
            }
            isProcessingTurn = false;
            isPlayerTurn = true;
        }
    
        #endregion
    
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
