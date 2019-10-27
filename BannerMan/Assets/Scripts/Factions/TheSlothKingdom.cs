using System;
using UnityEngine;

[Serializable]
public class TheSlothKingdom : Faction
{
    public override string GetName()
    {
        return "The Sloth Kingdom";
    }

    public override int GetWarriorMoveSpeed()
    {
        return BaseWarriorMoveSpeed / 2;
    }
    public override int GetHunterMoveSpeed()
    {
        return BaseHunterMoveSpeed / 2;
    }
    public override int GetTowerCost()
    {
        return BaseTowerCost / 2;
    }
}