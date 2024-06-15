using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Manager
{
    public class ProfilManager
    {
        public string[] deckAnimaux =new string[3];
        public string[] deckAnimauxbot =new string[3];
        public string[] deckAnimauxPlayer2 =new string[3];
        public string[] deckCartes = new string[8];
        public string[] deckCartesPlayer2 = new string[8];
        public string[] listeAnimauxPossible = { "panda", "dog", "turtle", "lion", "rat", "eagle" };

        public ProfilManager()
        {
            if (SkinManager.resSelection.Count == 0)
            {
                deckAnimaux = new[]
                {"panda", "dog", "turtle" };
            }
            if (SkinManager.resSelectionPlayer2.Count == 0)
            {
                deckAnimauxPlayer2 = new[]
                {"lion", "rat", "eagle" };
            }
            else
            {
                deckAnimaux =  SkinManager.resSelection.ToArray();
                deckAnimauxPlayer2 = SkinManager.resSelectionPlayer2.ToArray();
            }
            
            deckAnimauxbot = listeAnimauxPossible.OrderBy(x => Guid.NewGuid()).Take(3).ToArray();
            /**/
            if (CardManager.resSelection.Count == 0)
            {
                deckCartes = new[]
                    { "durian", "canon", "bat", "bombe",
                        "jump", "tomate", "knife", "heal"};
            }
            else
            {
                deckCartes =  CardManager.resSelection.ToArray();
                
            }
            
            if (CardManager.resSelectionPlayer2.Count == 0)
            {
                deckCartesPlayer2 = new[]
                    { "durian", "canon", "bat", "bombe",
                        "jump", "tomate", "knife", "heal"};
            }
            else
            {
                deckCartesPlayer2 = CardManager.resSelectionPlayer2.ToArray();
            }
            
            
            /*
            deckCartes = new[]
            {
                "durian", "canon", "bat", "bombe",
                "jump", "tomate", "knife", "heal"
            };
            */
            /*
            deckCartes = new[]
                        {
                            "durian", "durian", "durian", "durian",
                            "durian", "durian", "durian", "durian"
                        };
            */
        }
    }

}