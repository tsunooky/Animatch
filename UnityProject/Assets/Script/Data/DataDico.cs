using System.Collections.Generic;
using System;
namespace Script.Data
{
    public class DataDico
    {
        public static Dictionary<string, Type> animalTypes = new Dictionary<string, Type>
        {
            { "turtle", typeof(TurtleBehaviour) },
            { "panda", typeof(PandaBehaviour) },
            { "dog", typeof(DogBehaviour) },
            { "lion", typeof(LionBehaviour) }
        };
        
        public static Dictionary<string, Type> carteTypes = new Dictionary<string, Type>
        {
            { "tomate", typeof(TomateBehaviour) },
            {"bat",typeof(BatBehaviour)},
            {"jump",typeof(JumpBehaviour)},
            {"heal",typeof(HealBehaviour)},
            {"knife",typeof(KnifeBehaviour)}
        };
    }
}