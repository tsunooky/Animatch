using System;
using System.Collections.Generic;

namespace Script.Manager
{
    public class ProfilManager
    {
        private string _id;
        private string _pseudo;
        private string _mdp;
        private int _level;
        private Dictionary<string, int> _nbGame;
        private int _xp;
        private Dictionary<string, string> _traductionLangue;
        public string[] deckAnimaux = new string[3];
        public string[] deckCartes = new string[8];

        public ProfilManager(string pseudo, string mdp)
        {
            /* version non définitive
             il faudra relier avec Airtable*/
            _id = "BOB";
            _pseudo = pseudo;
            _mdp = mdp;
            _level = 1;
            _nbGame = new Dictionary<string, int>()
            {
                { "Victoire", 0 },
                { "MatchNul", 0 },
                { "Defaite", 0 }
            };
            _xp = 1;
            _traductionLangue = new Dictionary<string, string>()
            {
                { "a", "b" }
            };
            deckAnimaux = new[] {"turtle", "dog","lion"};
            deckCartes = new[]
            {
                "tomate", "tomate", "tomate", "tomate",
                "tomate", "tomate", "tomate", "tomate"
            };
        }

        public void sauvegarderProfil()
        {
            /* a relier avec Airtable*/
        }
    }

}