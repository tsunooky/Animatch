using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
   }
   
   public void UpdateSoundVolume(float volume)
   {
      audioMixer.SetFloat("SFXVolume", volume);
   }

}
