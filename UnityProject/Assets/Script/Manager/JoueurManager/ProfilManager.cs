using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Manager
{
    public class ProfilManager
    {
        public string[] deckAnimaux =new string[3];
        public string[] deckAnimauxbot =new string[3];
        public string[] deckAnimauxPlayer2 =new string[3];
        public string[] deckCartes = new string[8];

        public ProfilManager()
        {

            if (SkinManager.resSelection.Count == 0)
            {
                deckAnimaux = new[]
                {
                    "lion", "rat", "turtle"
                };
                deckAnimauxPlayer2 = new[]
                {
                    "dog", "panda", "turtle"
                };
            }
            else
            {
                deckAnimaux =  SkinManager.resSelection.ToArray();
                deckAnimauxPlayer2 = SkinManager.resSelectionPlayer2.ToArray();
            }
            
            deckAnimauxbot = new[]
            {
                "lion", "panda", "dog"
            };
            
            deckCartes = new[]
            {
                "durian", "canon", "bat", "heal",
                "jump", "tomate", "knife", "heal"
            };
        }

        public void sauvegarderProfil()
        {
            /* a relier avec Airtable*/
        }
    }

}