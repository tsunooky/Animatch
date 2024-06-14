using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : AnimalBehaviour
{
    public void Awake()
    {
        LoadData("rat");
    }

    public override void Animax()
    {
        player.drops += 2;
        player.MiseAjourAffichageDrops();
        player.enAction = false;
    }
}

