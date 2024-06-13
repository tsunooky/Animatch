using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr0;
    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    [CanBeNull] public SpriteRenderer sr0save = null;
    [CanBeNull] public SpriteRenderer sr1save = null;
    [CanBeNull] public SpriteRenderer sr2save = null;
    public List<Sprite> animals = new List<Sprite>();
    public List<int> selecteds = new List<int>(){0,1,2};
    public static List<string> resSelection = new List<string>();


    public void Start()
    {
        // Charger les sélections sauvegardées
        LoadSelectedAnimals();

        sr0.sprite = animals[selecteds[0]];
        sr1.sprite = animals[selecteds[1]];
        sr2.sprite = animals[selecteds[2]];
    }

    public void NextOption0()
    {
        selecteds[0] = selecteds[0] + 1;
        if (selecteds[0] == animals.Count)
        {
            selecteds[0] = 0;
        }
        sr0.sprite = animals[selecteds[0]];
    }
    
    public void PreviousOption0()
    {
        selecteds[0] = selecteds[0] - 1;
        if (selecteds[0] < 0)
        {
            selecteds[0] = animals.Count - 1;
        }
        sr0.sprite = animals[selecteds[0]];
    }
    
    public void NextOption1()
    {
        selecteds[1] = selecteds[1] + 1;
        if (selecteds[1] == animals.Count)
        {
            selecteds[1] = 0;
        }
        sr1.sprite = animals[selecteds[1]];
    }
    
    public void PreviousOption1()
    {
        selecteds[1] = selecteds[1] - 1;
        if (selecteds[1] < 0)
        {
            selecteds[1] = animals.Count - 1;
        }
        sr1.sprite = animals[selecteds[1]];
    }
    
    public void NextOption2()
    {
        selecteds[2] = selecteds[2] + 1;
        if (selecteds[2] == animals.Count)
        {
            selecteds[2] = 0;
        }
        sr2.sprite = animals[selecteds[2]];
    }
    
    public void PreviousOption2()
    {
        selecteds[2] = selecteds[2] - 1;
        if (selecteds[2] < 0)
        {
            selecteds[2] = animals.Count - 1;
        }
        sr2.sprite = animals[selecteds[2]];
    }

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
            }
        }

        resSelection = animalList;
        var res = "";
        foreach (var str in resSelection)
        {
            res += $"{str}" + " ";
        }
        SaveSelectedAnimals();
        Debug.Log(res);
        SceneManager.LoadScene("Menu");
    }
    
    private void SaveSelectedAnimals()
    {
        for (int i = 0; i < selecteds.Count; i++)
        {
            PlayerPrefs.SetInt($"SelectedAnimal{i}", selecteds[i]);
        }
        PlayerPrefs.Save(); // Sauvegarder les modifications dans PlayerPrefs
    }
    
    private void LoadSelectedAnimals()
    {
        for (int i = 0; i < selecteds.Count; i++)
        {
            if (PlayerPrefs.HasKey($"SelectedAnimal{i}"))
            {
                selecteds[i] = PlayerPrefs.GetInt($"SelectedAnimal{i}");
            }
            else
            {
                // Valeur par défaut si aucune sauvegarde n'est trouvée
                selecteds[i] = i;
            }
        }
    }
}
