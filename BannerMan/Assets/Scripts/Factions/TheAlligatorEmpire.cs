using System;
using UnityEngine;

[Serializable]
public class TheAlligatorEmpire : Faction
{
    public override string GetName()
    {
        return "The Alligator Empire";
    }

    public override int GetFarmHealth()
    {
        return BaseFarmHealth * 2;
    }
    public override int GetFarmCost()
    {
        return BaseFarmCost * 2;
    }
    public override int GetPlantationHealth()
    {
        return BasePlantationHealth * 2;
    }
    public override int GetPlantationCost()
    {
        return BasePlantationCost * 2;
    }
    public override int GetBarricadeHealth()
    {
        return BaseBarricadeHealth * 2;
    }
    public override int GetBarricadeCost()
    {
        return BaseBarricadeCost * 2;
    }
    public override int GetTowerHealth()
    {
        return BaseTowerHealth * 2;
    }
    public override int GetTowerCost()
    {
        return BaseTowerCost * 2;
    }
}