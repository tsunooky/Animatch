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
using Random = UnityEngine.Random;

namespace Script.Manager
{
    public class GameManager : AGameManager
    { 
        public GameObject nextTurnButton; 
        public PlayerManager bot;
        private bool animalBeingPlaced = false;
        public GameObject drop_left;
        
        
      
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GameManager n'est plus un singleton car il viens d'être redéfinis une deuxième fois !");
                return;
            }
            
            Instance = this;
            drop_left.gameObject.SetActive(false);
            nextTurnButton.gameObject.SetActive(false);
            joueur =  gameObject.AddComponent<PlayerManager>();
            joueur.CreateProfil();
            joueur.CreerMain();
            bot = gameObject.AddComponent<PlayerManager>();
            bot.IsBot = true;
            bot.CreateProfil();
            bot.IsBot = false;
            
            
            // Evité bug au lancement
            playerActif = joueur;
            tour = 1;
        }
    
        void Update()
        {
            
            if (spawn)
            {
                if (joueur.deckAnimal.Count == 0 && bot.deckAnimal.Count == 0)
                {
                    spawn = false;
                }
                else
                {
                    if (!animalBeingPlaced)
                    {
                        if (joueur.deckAnimal.Count > bot.deckAnimal.Count)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                if(joueur.enabled)
                                    StartCoroutine(PlaceAnimal(joueur));
                            }
                        }
                        else
                        {
                            if(bot.enabled)
                                StartCoroutine(PlaceAnimal(bot));
                        }
                    }
                    
                }
            }
            else
            {
                if (joueur.animaux_vivant.Count == 0)
                {
                    Win(bot,true);
                }
                if (bot.animaux_vivant.Count == 0)
                {
                    Win(joueur,false);
                }
                if (!tourActif)
                {
                    tourActif = true;
                    if (tour % 2 != 0)
                    {
                        if (joueur.enabled)
                        {
                            Debug.Log("C'est votre tour");
                            playerActif = joueur;
                            joueur.MiseAJourDrops(tour);
                            affichage_mana.text = $"{joueur.drops}";
                            nextTurnButton.gameObject.SetActive(true);
                            drop_left.gameObject.SetActive(true);
                            AnimalBehaviour animalActif = joueur.animaux_vivant.Dequeue();
                            joueur.animaux_vivant.Enqueue(animalActif);
                            playerActif.animalActif = animalActif;
                            animalActif.LoadAura();
                            joueur.MiseAjourAffichageDrops();    
                        }
                    }
                    else
                    {
                        if (bot.enabled)
                        {
                            playerActif = bot;
                            nextTurnButton.gameObject.SetActive(false);
                            drop_left.gameObject.SetActive(false);
                            affichage_mana.text = $" ? ";
                            Debug.Log("Tour du bot");
                            AnimalBehaviour animalActif = bot.animaux_vivant.Dequeue();
                            animalActif.LoadAura();
                            
                            playerActif.animalActif = animalActif;
                            
                            if (animalActif!=null)
                            { 
                                StartCoroutine(tirerbot(animalActif));
                            }
                            if (animalActif != null)
                            { 
                                bot.animaux_vivant.Enqueue(animalActif);
                            }    
                        }
                    }

                    Debug.Log("le joueur qui vient de jouer n'a plus que " + playerActif.drops+ " drops !");
                    joueur.MettreAjourMain();
                    tour += 1;
                }
            }
        }

        [CanBeNull]
        public AnimalBehaviour getnearest(PlayerManager player)
        {
            if (joueur.animaux_vivant.Count > 0)
            {
                AnimalBehaviour res = player.animaux_vivant.Peek();
                var moi = playerActif.animalActif.transform.position;
                float minDistance = float.MaxValue;

                foreach (var animal in player.animaux_vivant)
                {
                    float distance = Vector2.Distance(moi, animal.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        res = animal;
                    }
                }
                Debug.Log(res.nom);
                return res;
            }

            return null;
        }
        
        

        IEnumerator tirerbot(AnimalBehaviour animalActif)
        {
            yield return new WaitForSeconds(2);
            if (animalActif != null)
            {
                var aimbotani = animalActif.AddComponent<AimBot>();
                var cible = getnearest(joueur);
                Vector2 botPosition = gameObject.transform.position;
                Vector2 ciblePosition = cible.transform.position;
                Vector2 relativePosition = ciblePosition - botPosition;

                if (relativePosition.x > 0)
                {
                    aimbotani.TirerDansUneDirectiondroite(ciblePosition);
                }
                else if (relativePosition.x < 0)
                {
                    aimbotani.TirerDansUneDirectiongauche(ciblePosition);
                }

                Destroy(aimbotani);
                FinDuTour();
            }
        }
        
        IEnumerator PlaceAnimal(PlayerManager player)
        {
            animalBeingPlaced = true;

            if (player == joueur)
            {
                bool posval = false;
                Vector2 mousePosition = Vector2.one;
                while (!posval)
                {
                    // Pour le joueur, attendre un clic de souris
                    while (!Input.GetMouseButtonDown(0))
                    {
                        yield return null;
                    }

                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Collider2D? hit = Physics2D.OverlapPoint(mousePosition);
                    if (hit != null)
                    {
                        Debug.Log("Collision detecté, ne peut pas faire spawn ici.");
                    }
                    else
                    {
                        posval = true;
                    } 
                    yield return new WaitForSeconds(0.1f);
                }

                // Instanciez l'animal à la position du clic en x et y = hauteur
                AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, joueur.deckAnimal.Dequeue(),player);
                joueur.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = joueur;
                newAnimal.LoadHealthbar();
            }
            else
            {
                // Pour le bot, attendre avant de placer l'animal
                yield return new WaitForSeconds(2f); // Attendre 2 secondes

                float randomX = UnityEngine.Random.Range(-6.5f, 7f);
                AnimalBehaviour newAnimal = creerAnimal(randomX, 4.7f, bot.deckAnimal.Dequeue(),player);
                bot.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = bot;
                newAnimal.LoadHealthbar();
            }

            player.animalActif = player.animaux_vivant.Peek();

            animalBeingPlaced = false;
        }
    }

    
}
