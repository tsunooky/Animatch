using System;
using System.Collections.Generic;

namespace Script.Manager
{
    public class ProfilManager
    {
        public string[] deckAnimaux =new string[3];
        public string[] deckAnimauxbot =new string[3];
        public string[] deckCartes = new string[8];

        public ProfilManager()
        {

            if (SkinManager.resSelection.Count == 0)
            {
                deckAnimaux = new[]
                {
                    "dog", "panda", "turtle"
                };
            }
            else
            {
                deckAnimaux =  SkinManager.resSelection.ToArray();
            }
            
            deckAnimauxbot = new[]
            {
                "panda", "panda", "panda"
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