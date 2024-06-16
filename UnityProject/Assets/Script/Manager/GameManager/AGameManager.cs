using System;
using System.Collections;
using Script.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.Manager

{
    public abstract class AGameManager : MonoBehaviour
    {
        public PlayerManager? joueur;
        public int tour;
        protected bool spawn = true;
        public static AGameManager Instance;
        public PlayerManager playerActif;
        public bool tourActif;
        public Text affichage_mana;

        public GameObject settings_canvas;
        public GameObject settings;
        public GameObject animax;
        public GameObject passif;
        public Text Turn;
        
        [SerializeField] private GameObject _startingTransition;
        [SerializeField] private GameObject _endingTransition;

        public void Start()
        {
            _startingTransition.SetActive(true);
            StartCoroutine(DisableStartingSceneTransition(3f));
        }

        private IEnumerator DisableStartingSceneTransition(float delay)
        {
            yield return new WaitForSeconds(delay);
            _startingTransition.SetActive(false);
        }
   
   
        private IEnumerator WaitAndLoadScene(string sceneName,float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
            Start();
        }
        
        #region Code Moved
        
        
        protected AnimalBehaviour creerAnimal(float x, float y,string animal,PlayerManager player)
        {
            var animalTypes = DataDico.animalTypes;
            if (animalTypes.ContainsKey(animal))
            {
                
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