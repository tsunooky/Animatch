using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    public Text victoireBot;
    public Text VictoireJoueur;
    
   
    
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

        // Changer la scène après avoir affiché le message
        */
        SceneManager.LoadScene("Fin");
        Invoke("QuitGame", 10f);
    }

    // Quitte le jeu
    protected void QuitGame()
    {
        Application.Quit();
    }
}