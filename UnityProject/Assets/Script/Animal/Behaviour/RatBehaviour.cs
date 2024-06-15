using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : AnimalBehaviour
{
    public void Awake()
    {
        LoadData("rat");
        DefAnimax += "It Get 2 drops more.";
        DefPassive += "When ending his turn, it gets 1 drop more.";
    }

    public override void Animax()
    {
        player.drops += 2;
        player.MiseAjourAffichageDrops();
        player.enAction = false;
    }
}

