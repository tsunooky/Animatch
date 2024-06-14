using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr0;
    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    public SpriteRenderer sr3;
    public SpriteRenderer sr4;
    public SpriteRenderer sr5;
    public static bool fix = false;
    
    public List<Sprite> animals = new List<Sprite>();
    
    public List<int> selecteds = new List<int>(){0, 1, 2}; 
    public List<int> selecteds2 = new List<int>(){3, 4, 5}; 
    
    public static List<string> resSelection = new List<string>();
    public static List<string> resSelectionPlayer2 = new List<string>();


    private void Start()
    {
        LoadSelectedAnimals("Team1", selecteds);
        LoadSelectedAnimals("Team2", selecteds2);
        
        sr0.sprite = animals[selecteds[0]];
        sr1.sprite = animals[selecteds[1]];
        sr2.sprite = animals[selecteds[2]];
        
        sr3.sprite = animals[selecteds2[0]];
        sr4.sprite = animals[selecteds2[1]];
        sr5.sprite = animals[selecteds2[2]];
    }
    
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
        if (fix == false)
        {
            Debug.Log("freuuuureeee");
            resSelectionPlayer2 = animalList;
            SaveSelectedAnimals("Team2", selecteds2);
        }
        SaveSelectedAnimals("Team1", selecteds);
        SceneManager.LoadScene("Menu");
    }

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
    
    public void ReturnToPlayer1()
    {
        fix = true;
        Debug.Log("fix = true");
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

    private void SaveSelectedAnimals(string team, List<int> selections)
    {
        for (int i = 0; i < selections.Count; i++)
        {
            PlayerPrefs.SetInt($"{team}_SelectedAnimal{i}", selections[i]);
        }
        PlayerPrefs.Save();
    }

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
                selections[i] = i;
            }
        }
    }
}
