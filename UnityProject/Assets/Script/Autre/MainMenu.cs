using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGameSolo()
   {
      SceneManager.LoadScene("Main");
   }
   public void PlayGameMulti()
   {
      SceneManager.LoadScene("Main_Online");
   }
   public void OpenOption()
   {
      SceneManager.LoadScene("Options");
   }
   
   public void OpenEquipes()
   {
      SceneManager.LoadScene("Equipes");
   }
   
   public void OpenMenu()
   {
      SceneManager.LoadScene("MainMenu");
   }

   public void QuitGame()
   {
      Application.Quit();
   }

}
