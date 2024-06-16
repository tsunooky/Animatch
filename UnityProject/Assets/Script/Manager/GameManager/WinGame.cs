using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Script.Manager;
using static Script.Manager.AGameManager;

public class WinGame : MonoBehaviour
{
    public void Win(PlayerManager player, bool res)
    {/*
        Debug.Log("Victoire de " + player.name);
        if (res)
        {
            victoireBot.gameObject.SetActive(true);
        }
        else
        {
            if (VictoireJoueur != null)
            {
                VictoireJoueur.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogError("VictoireJoueur n'est pas assigné dans l'Inspecteur.");
            }
        }

       
        */
        GameData.Winner = player;
        SceneManager.LoadScene("Fin");
        Invoke("QuitGame", 10f);
    }

  
    protected void QuitGame()
    {
        Application.Quit();
    }
}