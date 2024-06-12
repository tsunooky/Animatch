using System;
using System.Collections.Generic;

namespace Script.Manager
{
    public class ProfilManager
    {
        public string[] deckAnimaux = new string[3];
        public string[] deckCartes = new string[8];

        public ProfilManager(string pseudo, string mdp)
        {
            deckAnimaux = new[]
            {
                "panda", "dog","turtle"
            };
            
            deckCartes = new[]
            {
                "jump", "tomate", "knife", "heal",
                "jump", "tomate", "knife", "heal"
            };
        }

        public void sauvegarderProfil()
        {
            /* a relier avec Airtable*/
        }
    }

}