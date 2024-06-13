using System.Collections.Generic;
using System;
using System.Collections;
using JetBrains.Annotations;
using Script.Data;
using UnityEngine;
using Script.Manager;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;
using static Unity.Mathematics.Random;
using static Unity.Mathematics.math;

namespace Script.ManagerOnline
{
    public class GameManagerOnline : AGameManager
    { 
        [CanBeNull] public PlayerManager joueur2;
        public GameObject nextTurnButton;
        private bool animalBeingPlaced = false;
        public GameObject drop_left;
        private bool isRoomReady = false;
        public Text Waiting;
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

            drop_left.gameObject.SetActive(false);
            nextTurnButton.gameObject.SetActive(false);
            Waiting.text = "Waiting another player to get connect to the room to start !";
            Waiting.enabled = true;
            Instance = this;
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    GameObject player1 = PhotonNetwork.Instantiate("Prefabs/Autre/Player", Vector3.zero, Quaternion.identity);
                    joueur = player1.GetComponent<PlayerManager>();
                    joueur.CreateProfil();
                    joueur.CreerMain();
                }
                else
                {
                    GameObject player2 = PhotonNetwork.Instantiate("Prefabs/Autre/Player", Vector3.zero, Quaternion.identity);
                    joueur2 = player2.GetComponent<PlayerManager>();
                    joueur2.CreateProfil();
                    joueur2.CreerMain();
                }
            }
        }
    
        void Update()
        {
            if (spawn)
            {
                if (joueur.deckAnimal.Count == 0 && joueur2.deckAnimal.Count == 0)
                {
                    spawn = false;
                }
                else
                {
                    if (!animalBeingPlaced)
                    {
                        if (joueur.deckAnimal.Count > joueur2.deckAnimal.Count)
                        {
                            if (Input.GetMouseButtonDown(0))
                                StartCoroutine(PlaceAnimal(joueur));
                        }
                        else
                        {
                            if(Input.GetMouseButtonDown(0))
                                StartCoroutine(PlaceAnimal(joueur2));
                        }
                    }
                }
            }
            else
            {
                if (joueur.animaux_vivant.Count == 0)
                {
                    Win(joueur2, true);
                }
                if (joueur2.animaux_vivant.Count == 0)
                {
                    Win(joueur, false);
                }
                if (!tourActif)
                {
                    tourActif = true;
                    if (tour % 2 != 0)
                    {
                        if (PhotonNetwork.IsMasterClient)
                        {
                            StartCoroutine(GererTour(joueur));
                        }
                    }
                    else
                    {
                        if (!PhotonNetwork.IsMasterClient)
                        {
                            StartCoroutine(GererTour(joueur2));
                        }
                    }
                }
            }
        }
        
        IEnumerator PlaceAnimal(PlayerManager player)
        {
            animalBeingPlaced = true;

            if (player == joueur)
            {
                // Pour le joueur, attendre un clic de souris
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null;
                }

                // Obtenez les coordonnées du clic de la souris
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Instanciez l'animal à la position du clic en x et y = hauteur
                AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, joueur.deckAnimal.Dequeue(),player);
                joueur.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = joueur;
                newAnimal.LoadHealthbar();
            }
            else
            {
                // Pour le joueur, attendre un clic de souris
                while (!Input.GetMouseButtonDown(0))
                {
                    yield return null;
                }

                // Obtenez les coordonnées du clic de la souris
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                // Instanciez l'animal à la position du clic en x et y = hauteur
                AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, joueur2.deckAnimal.Dequeue(),player);
                joueur2.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = joueur2;
                newAnimal.LoadHealthbar();
            }

            player.animalActif = player.animaux_vivant.Peek();

            animalBeingPlaced = false;
        }

        #region PunCode
        [PunRPC]
        void PlaceAnimalRPC(Player player, float x, float y)
        {
            // Utiliser le joueur spécifié pour placer l'animal
            PlayerManager playerManager = (player == PhotonNetwork.LocalPlayer) ? joueur : joueur2;
            StartCoroutine(PlaceAnimal(playerManager));
        }
        

        #endregion
        #region GererJoueur
        
        private IEnumerator GererTour(PlayerManager player)
        {
            isProcessingTurn = true;
            playerActif = player;
            Tour.text = $"C'est le tour du {player.name}";
            Tour.enabled = true;

            yield return new WaitForSeconds(3);

            if (player.animaux_vivant.Count == 0)
            {
                Win(player == joueur ? joueur2 : joueur, player == joueur ? false : true);
            }
            else
            {
                AnimalBehaviour animalActif = player.animaux_vivant.Dequeue();
                player.animaux_vivant.Enqueue(animalActif);
                player.animalActif = animalActif;
                animalActif.LoadAura();
                player.MiseAjourAffichageDrops();
            }
            isProcessingTurn = false;
            isPlayerTurn = !isPlayerTurn;
            tourActif = false;
            tour++;
        }
        #endregion

        #region 2Player
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            CheckRoomStatus();
        }
        
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            CheckRoomStatus(); 
        }

        private void CheckRoomStatus()
        {
            isRoomReady = PhotonNetwork.CurrentRoom.PlayerCount > 1; 
            Waiting.enabled = !isRoomReady;
        }
        #endregion
    }
    /*
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
     */
}