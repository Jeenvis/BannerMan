using System;
using UnityEngine;

[Serializable]
public class TheCatCoalition : Faction
{
    public override string GetName()
    {
        return "The Cat Coalition";
    }

    /* public override int GetFarmCost()
     {
         if (Time.time > 30)
         {
             return 0;
         }
         return 10;
}*/
}