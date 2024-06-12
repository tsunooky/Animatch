using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr0;
    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    public List<Sprite> animals = new List<Sprite>();
    public List<int> selecteds = new List<int>(){0,1,2};
    public GameObject animal1Skin;

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
            }
        }
        
        Console.WriteLine(animalList[2]);
        SceneManager.LoadScene("Menu");
    }
}
