using System;
using UnityEngine;

[Serializable]
public class TheElephantElders : Faction
{
    public override string GetName()
    {
        return "The Elephant Elders";
    }

    public override int GetWarriorMoveSpeed()
    {
        return BaseWarriorMoveSpeed / 2;
    }
    public override int GetWarriorHealth()
    {
        return BaseWarriorHealth * 2;
    }
    public override int GetHunterMoveSpeed()
    {
        return BaseHunterMoveSpeed / 2;
    }
    public override int GetHunterHealth()
    {
        return BaseHunterHealth * 2;
    }
}