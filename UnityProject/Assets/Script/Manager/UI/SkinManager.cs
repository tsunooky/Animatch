using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    // SpriteRenderers pour les animaux des deux équipes
    public SpriteRenderer sr0;
    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    public SpriteRenderer sr3;
    public SpriteRenderer sr4;
    public SpriteRenderer sr5;

    // Liste de tous les sprites d'animaux
    public List<Sprite> animals = new List<Sprite>();

    // Sélections d'animaux pour les deux équipes
    public List<int> selecteds = new List<int>(){0, 1, 2}; // Pour Team1
    public List<int> selecteds2 = new List<int>(){0, 1, 2}; // Pour Team2

    // Listes statiques pour stocker les noms des animaux sélectionnés
    public static List<string> resSelection = new List<string>(); // Pour Team1
    public static List<string> resSelectionPlayer2 = new List<string>(); // Pour Team2

    // Méthode appelée au démarrage de la scène
    private void Start()
    {
        // Charger les sélections sauvegardées
        LoadSelectedAnimals("Team1", selecteds);
        LoadSelectedAnimals("Team2", selecteds2);

        // Assigner les sprites pour Team1
        sr0.sprite = animals[selecteds[0]];
        sr1.sprite = animals[selecteds[1]];
        sr2.sprite = animals[selecteds[2]];

        // Assigner les sprites pour Team2
        sr3.sprite = animals[selecteds2[0]];
        sr4.sprite = animals[selecteds2[1]];
        sr5.sprite = animals[selecteds2[2]];
    }

    // Méthodes de navigation pour les sélections de Team1
    public void NextOption0()
    {
        selecteds[0] = (selecteds[0] + 1) % animals.Count;
        sr0.sprite = animals[selecteds[0]];
    }
    
    public void PreviousOption0()
    {
        selecteds[0] = (selecteds[0] - 1 + animals.Count) % animals.Count;
        sr0.sprite = animals[selecteds[0]];
    }
    
    public void NextOption1()
    {
        selecteds[1] = (selecteds[1] + 1) % animals.Count;
        sr1.sprite = animals[selecteds[1]];
    }
    
    public void PreviousOption1()
    {
        selecteds[1] = (selecteds[1] - 1 + animals.Count) % animals.Count;
        sr1.sprite = animals[selecteds[1]];
    }
    
    public void NextOption2()
    {
        selecteds[2] = (selecteds[2] + 1) % animals.Count;
        sr2.sprite = animals[selecteds[2]];
    }
    
    public void PreviousOption2()
    {
        selecteds[2] = (selecteds[2] - 1 + animals.Count) % animals.Count;
        sr2.sprite = animals[selecteds[2]];
    }

    // Méthodes de navigation pour les sélections de Team2
    public void NextOption0Player2()
    {
        selecteds2[0] = (selecteds2[0] + 1) % animals.Count;
        sr3.sprite = animals[selecteds2[0]];
    }
    
    public void PreviousOption0Player2()
    {
        selecteds2[0] = (selecteds2[0] - 1 + animals.Count) % animals.Count;
        sr3.sprite = animals[selecteds2[0]];
    }
    
    public void NextOption1Player2()
    {
        selecteds2[1] = (selecteds2[1] + 1) % animals.Count;
        sr4.sprite = animals[selecteds2[1]];
    }
    
    public void PreviousOption1Player2()
    {
        selecteds2[1] = (selecteds2[1] - 1 + animals.Count) % animals.Count;
        sr4.sprite = animals[selecteds2[1]];
    }
    
    public void NextOption2Player2()
    {
        selecteds2[2] = (selecteds2[2] + 1) % animals.Count;
        sr5.sprite = animals[selecteds2[2]];
    }
    
    public void PreviousOption2Player2()
    {
        selecteds2[2] = (selecteds2[2] - 1 + animals.Count) % animals.Count;
        sr5.sprite = animals[selecteds2[2]];
    }

    // Sauvegarder les sélections et retourner au menu principal
    public void ReturnMenu()
    {
        List<string> animalList = new List<string>();
        foreach (var e in selecteds)
        {
            switch (e)
            {
                case 0:
                    animalList.Add("panda");
                    break;
                case 1:
                    animalList.Add("dog");
                    break;
                case 2:
                    animalList.Add("turtle");
                    break;
                case 3:
                    animalList.Add("rat");
                    break;
                case 4:
                    animalList.Add("lion");
                    break;
                case 5:
                    animalList.Add("eagle");
                    break;
            }
        }

        resSelection = animalList;
        SaveSelectedAnimals("Team1", selecteds);
        SceneManager.LoadScene("Menu");
    }

    // Sauvegarder les sélections de Team1 et passer à la scène de Team2
    public void GoToPlayer2()
    {
        List<string> animalList = new List<string>();
        foreach (var e in selecteds)
        {
            switch (e)
            {
                case 0:
                    animalList.Add("panda");
                    break;
                case 1:
                    animalList.Add("dog");
                    break;
                case 2:
                    animalList.Add("turtle");
                    break;
                case 3:
                    animalList.Add("rat");
                    break;
                case 4:
                    animalList.Add("lion");
                    break;
                case 5:
                    animalList.Add("eagle");
                    break;
            }
        }

        resSelection = animalList;
        SaveSelectedAnimals("Team1", selecteds);
        SceneManager.LoadScene("Team2");
    }

    // Sauvegarder les sélections de Team2 et retourner à la scène de Team1
    public void ReturnToPlayer1()
    {
        List<string> animalList = new List<string>();
        foreach (var e in selecteds2)
        {
            switch (e)
            {
                case 0:
                    animalList.Add("panda");
                    break;
                case 1:
                    animalList.Add("dog");
                    break;
                case 2:
                    animalList.Add("turtle");
                    break;
                case 3:
                    animalList.Add("rat");
                    break;
                case 4:
                    animalList.Add("lion");
                    break;
                case 5:
                    animalList.Add("eagle");
                    break;
            }
        }

        resSelectionPlayer2 = animalList;
        SaveSelectedAnimals("Team2", selecteds2);
        SceneManager.LoadScene("Team");
    }

    // Méthode générique pour sauvegarder les sélections dans PlayerPrefs
    private void SaveSelectedAnimals(string team, List<int> selections)
    {
        for (int i = 0; i < selections.Count; i++)
        {
            PlayerPrefs.SetInt($"{team}_SelectedAnimal{i}", selections[i]);
        }
        PlayerPrefs.Save(); // Sauvegarder les modifications dans PlayerPrefs
    }

    // Méthode générique pour charger les sélections depuis PlayerPrefs
    private void LoadSelectedAnimals(string team, List<int> selections)
    {
        for (int i = 0; i < selections.Count; i++)
        {
            if (PlayerPrefs.HasKey($"{team}_SelectedAnimal{i}"))
            {
                selections[i] = PlayerPrefs.GetInt($"{team}_SelectedAnimal{i}");
            }
            else
            {
                // Valeur par défaut si aucune sauvegarde n'est trouvée
                selections[i] = i;
            }
        }
    }
}
