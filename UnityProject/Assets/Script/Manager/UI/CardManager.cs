using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    #region Init
    public SpriteRenderer sr0;
    public SpriteRenderer sr1;
    public SpriteRenderer sr2;
    public SpriteRenderer sr3;
    public SpriteRenderer sr4;
    public SpriteRenderer sr5;
    public SpriteRenderer sr6;
    public SpriteRenderer sr7;
    /*
    public SpriteRenderer sr8;
    public SpriteRenderer sr9;
    public SpriteRenderer sr10;
    public SpriteRenderer sr11;
    public SpriteRenderer sr12;
    public SpriteRenderer sr13;
    public SpriteRenderer sr14;
    public SpriteRenderer sr15;*/
    public static bool fix = false;
    public List<Sprite> cards = new List<Sprite>();
    public List<int> selecteds = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7}; 
   /* public List<int> selecteds2 = new List<int>(){8, 9, 10, 11, 12, 13, 14, 15}; 
    */public static List<string> resSelection = new List<string>();
    /*public static List<string> resSelectionPlayer2 = new List<string>();
*/
    #endregion
    void Start()
    {
        LoadSelectedCards("Hand1", selecteds);
        /*LoadSelectedCards("Hand2", selecteds2);*/

        sr0.sprite = cards[selecteds[0]];
        sr1.sprite = cards[selecteds[1]];
        sr2.sprite = cards[selecteds[2]];
        sr3.sprite = cards[selecteds[3]];
        sr4.sprite = cards[selecteds[4]];
        sr5.sprite = cards[selecteds[5]];
        sr6.sprite = cards[selecteds[6]];
        sr7.sprite = cards[selecteds[7]];
        /*
        sr8.sprite = cards[selecteds2[0]];
        sr9.sprite = cards[selecteds2[1]];
        sr10.sprite = cards[selecteds2[2]];
        sr11.sprite = cards[selecteds2[3]];
        sr12.sprite = cards[selecteds2[4]];
        sr13.sprite = cards[selecteds2[5]];
        sr14.sprite = cards[selecteds2[6]];
        sr15.sprite = cards[selecteds2[7]];*/
    }
    void Update()
    {
        
    }
    private void LoadSelectedCards(string hand, List<int> selections)
    {
        for (int i = 0; i < selections.Count; i++)
        {
            if (PlayerPrefs.HasKey($"{hand}_SelectedCards{i}"))
            {
                selections[i] = PlayerPrefs.GetInt($"{hand}_SelectedCards{i}");
            }
            else
            {
                selections[i] = i;
            }
        }
    }
    private void SaveSelectedCards(string hand, List<int> selections)
    {
        for (int i = 0; i < selections.Count; i++)
        {
            PlayerPrefs.SetInt($"{hand}_SelectedAnimal{i}", selections[i]);
        }
        PlayerPrefs.Save();
    }
    
    public void ReturnMenu()
    {
        List<string> cardList = new List<string>();
        foreach (var e in selecteds)
        {
            switch (e)
            {
                case 0:
                    cardList.Add("tomate");
                    break;
                case 1:
                    cardList.Add("heal");
                    break;
                case 2:
                    cardList.Add("knife");
                    break;
                case 3:
                    cardList.Add("jump");
                    break;
                case 4:
                    cardList.Add("durian");
                    break;
                case 5:
                    cardList.Add("canon");
                    break;
                case 6:
                    cardList.Add("bombe");
                    break;
                case 7:
                    cardList.Add("bat");
                    break;
            }
        }
        resSelection = cardList;
        if (fix == false)
        {
            Debug.Log("freuuuureeee");
           /* resSelectionPlayer2 = cardList;
            SaveSelectedCards("Hand2", selecteds2);*/
        }
        SaveSelectedCards("Hand1", selecteds);
        SceneManager.LoadScene("Menu");
    }
    
    public void NextOption0()
    {
        selecteds[0] = (selecteds[0] + 1) % cards.Count;
        sr0.sprite = cards[selecteds[0]];
    }
    
    public void PreviousOption0()
    {
        selecteds[0] = (selecteds[0] - 1 + cards.Count) % cards.Count;
        sr0.sprite = cards[selecteds[0]];
    }
    
    public void NextOption1()
    {
        selecteds[1] = (selecteds[1] + 1) % cards.Count;
        sr1.sprite = cards[selecteds[1]];
    }
    
    public void PreviousOption1()
    {
        selecteds[1] = (selecteds[1] - 1 + cards.Count) % cards.Count;
        sr1.sprite = cards[selecteds[1]];
    }
    
    public void NextOption2()
    {
        selecteds[2] = (selecteds[2] + 1) % cards.Count;
        sr2.sprite = cards[selecteds[2]];
    }
    
    public void PreviousOption2()
    {
        selecteds[2] = (selecteds[2] - 1 + cards.Count) % cards.Count;
        sr2.sprite = cards[selecteds[2]];
    }
    public void NextOption3()
    {
        selecteds[3] = (selecteds[3] + 1) % cards.Count;
        sr3.sprite = cards[selecteds[3]];
    }

    public void PreviousOption3()
    {
        selecteds[3] = (selecteds[3] - 1 + cards.Count) % cards.Count;
        sr3.sprite = cards[selecteds[3]];
    }

    public void NextOption4()
    {
        selecteds[4] = (selecteds[4] + 1) % cards.Count;
        sr4.sprite = cards[selecteds[4]];
    }

    public void PreviousOption4()
    {
        selecteds[4] = (selecteds[4] - 1 + cards.Count) % cards.Count;
        sr4.sprite = cards[selecteds[4]];
    }

    public void NextOption5()
    {
        selecteds[5] = (selecteds[5] + 1) % cards.Count;
        sr5.sprite = cards[selecteds[5]];
    }

    public void PreviousOption5()
    {
        selecteds[5] = (selecteds[5] - 1 + cards.Count) % cards.Count;
        sr5.sprite = cards[selecteds[5]];
    }

    public void NextOption6()
    {
        selecteds[6] = (selecteds[6] + 1) % cards.Count;
        sr6.sprite = cards[selecteds[6]];
    }

    public void PreviousOption6()
    {
        selecteds[6] = (selecteds[6] - 1 + cards.Count) % cards.Count;
        sr6.sprite = cards[selecteds[6]];
    }

    public void NextOption7()
    {
        selecteds[7] = (selecteds[7] + 1) % cards.Count;
        sr7.sprite = cards[selecteds[7]];
    }

    public void PreviousOption7()
    {
        selecteds[7] = (selecteds[7] - 1 + cards.Count) % cards.Count;
        sr7.sprite = cards[selecteds[7]];
    }

    
    
}
