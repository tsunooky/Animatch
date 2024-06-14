using System.Collections.Generic;
using System;
using System.Collections;
using JetBrains.Annotations;
using Script.Data;
using UnityEngine;
using Script.Manager;
using UnityEngine.UI;
using static Script.Manager.AGameManager;

namespace Script.Manager
{
    public class End : MonoBehaviour
    {
        public Text Winner;
        
        public void Awake()
        {
            if (Instance is GameManager gameManager)
            {
                if (GameData.Winner == gameManager.joueur)
                    Winner.text = "Victory of the Player !";
                else
                    Winner.text = "Victory of Bob the Bot !";
            }
            else if (Instance is GameManager2J gameManager2)
            {
                if (GameData.Winner == gameManager2.joueur)
                    Winner.text = "Victory of the Player 1!";
                else
                    Winner.text = "Victory of the Player 2!";
            }
            else
                Winner.text = "Well done, You've Won !";
        }
    }
    
}