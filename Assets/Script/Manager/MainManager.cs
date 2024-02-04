using UnityEngine;
using System;
using Script.Manager;

public class MainManager : MonoBehaviour
{
    public PlayerManager player;

    private void Start()
    {
        player = GameManager.Instance.joueur;
    }

    void Update()
    {
        if (player != null && player.main != null && player.main.Count != 0)
        {
            var typeCarte = player.main.Dequeue();
            var a = gameObject.GetComponent(typeCarte);
            if (a == null)
            {
                gameObject.AddComponent(typeCarte);
            }
            player.main.Enqueue(typeCarte);
        }
    }
}