using System.Collections.Generic;
using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

namespace Script.Manager
{
    public class GameManager : AGameManager
    { 
        public GameObject nextTurnButton; 
        public PlayerManager bot;
        private bool animalBeingPlaced = false;
        public GameObject drop_left;
        public Button Settings;
      
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("GameManager n'est plus un singleton car il viens d'être redéfinis une deuxième fois !");
                return;
            }

            Settings.interactable = false;
            Settings.enabled = false;
            settings.SetActive(false);
            settings_canvas.SetActive(false);
            animax.SetActive(false);
            passif.SetActive(false);
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
            // Eviter bug au lancement
            playerActif = joueur;
            tour = 1;
            foreach (var cartes in joueur.mainManager)
            {
                cartes.SetActive(false);
            }
        }
    
        void Update()
        {
            if (spawn)
            {
                if (joueur.deckAnimal.Count == 0 && bot.deckAnimal.Count == 0)
                {
                    settings.SetActive(true);
                    settings_canvas.SetActive(true);
                    Settings.interactable = true;
                    Settings.enabled = true;
                    spawn = false;
                    foreach (var cartes in joueur.mainManager)
                    {
                        cartes.SetActive(true);
                    }
                }
                else
                {
                    if (!animalBeingPlaced)
                    {
                        if (joueur.deckAnimal.Count > bot.deckAnimal.Count)
                        {
                            Turn.text = "Player 1 for spawn";
                            if (Input.GetMouseButtonDown(0))
                            {
                                if(joueur.enabled)
                                    StartCoroutine(PlaceAnimal(joueur));
                            }
                        }
                        else
                        {
                            Turn.text = "Bob the bot for spawn";
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
                    Turn.text = $"Turn n°{tour}.";
                    if (tour % 2 != 0)
                    {
                        if (joueur.enabled)
                        {
                            Wait2sec();
                            Debug.Log("C'est votre tour");
                            animax.SetActive(true);
                            passif.SetActive(true);
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
                            animax.SetActive(false);
                            passif.SetActive(false);
                            playerActif = bot;
                            nextTurnButton.gameObject.SetActive(false);
                            drop_left.gameObject.SetActive(false);
                            affichage_mana.text = $" ? ";
                            Debug.Log("Tour du bot");
                            AnimalBehaviour animalActif = bot.animaux_vivant.Dequeue();
                            
                            playerActif.animalActif = animalActif;
                            animalActif.LoadAura();
                            Wait2sec();
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
        public AnimalBehaviour GetNearest(PlayerManager player)
        {
            if (joueur.animaux_vivant.Count == 0)
            {
                return null;
            }

            AnimalBehaviour nearestAnimal = player.animaux_vivant.Peek();
            var currentPosition = playerActif.animalActif.transform.position;
            float minDistance = float.MaxValue;

            foreach (var animal in player.animaux_vivant)
            {
                Vector2 relativePosition = animal.transform.position - currentPosition;

                float distance = relativePosition.magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestAnimal = animal;
                }
            }

            Debug.Log(nearestAnimal.nom);
            return nearestAnimal;
        }


        IEnumerator Wait2sec()
        {
            yield return new WaitForSeconds(2f);
        }
        


        IEnumerator tirerbot(AnimalBehaviour animalActif)
        {
            yield return new WaitForSeconds(2);
            if (animalActif != null)
            {
                var aimbotani = animalActif.AddComponent<AimBot>();
                var cible = GetNearest(joueur);
                Vector2 botPosition = animalActif.transform.position;
                Vector2 ciblePosition = cible.transform.position;
                Vector2 relativePosition = ciblePosition - botPosition;
                Debug.Log($"bot x = {botPosition.x}, bot y = {botPosition.y}");
                Debug.Log($"cible x = {ciblePosition.x}, cible y = {ciblePosition.y}");
                Debug.Log($"relative x = {relativePosition.x}, relative y = {relativePosition.y}");
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

               
                AnimalBehaviour newAnimal = creerAnimal(mousePosition.x, mousePosition.y, joueur.deckAnimal.Dequeue(),player);
                joueur.animaux_vivant.Enqueue(newAnimal);
                newAnimal.player = joueur;
                newAnimal.LoadHealthbar();
            }
            else
            {
               //le bot att avant de spawn
                yield return new WaitForSeconds(1.5f); 

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
