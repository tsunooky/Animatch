using System.Collections;
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using static Script.Manager.AGameManager;

public class MainMenu : MonoBehaviour
{
   public AudioMixer audioMixer;
   
   public void PlayGameSolo()
   {
      SceneManager.LoadScene("Main");
   }
   public void PlayGameMulti()
   {
      SceneManager.LoadScene("Main 2P");
   }
   public void OpenOption()
   {
      SceneManager.LoadScene("Settings");
   }
   public void OpenOptionInGame()
   {
      Instance.joueur.enabled = false;
      if (Instance is GameManager gameManager)
         gameManager.bot.enabled = false;
      if (Instance is GameManager2J gameManager2)
         gameManager2.joueur2.enabled = false;
      SceneManager.LoadScene("Settingsgame",LoadSceneMode.Additive);
   }
   
   public void OpenEquipes()
   {
      SceneManager.LoadScene("Team");
   }
   
   public void OpenMenu()
   {
      SceneManager.LoadScene("Menu");
   }
   public void OpenInPlayGame()
   {
      SceneManager.LoadScene("Main");
   }

   public void QuitGame()
   {
      Application.Quit();
   }
   
   public void CloseSettings()
   {
      SceneManager.UnloadSceneAsync("Settingsgame");
      Instance.joueur.enabled = true;
      if (Instance is GameManager gameManager)
         gameManager.bot.enabled = true;
      if (Instance is GameManager2J gameManager2)
         gameManager2.joueur2.enabled = true;
   }
   
   public void UpdateSoundVolume(float volume)
   {
      audioMixer.SetFloat("SFXVolume", volume);
   }

}
