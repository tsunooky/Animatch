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
      SceneManager.LoadScene("Main 2P");
   }
   public void OpenOption()
   {
      SceneManager.LoadScene("Settings");
   }
   
   public void OpenEquipes()
   {
      SceneManager.LoadScene("Team");
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
