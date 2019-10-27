using System;

[Serializable]
public abstract class Faction
{
    //ADDITIONAL RULES:
    //name
    public abstract string GetName(); //Must be filled by the faction script object
    //can build units everywhere
    //can build buildings everywhere

    //CURSOR STATS:
    public int BaseCursorSpeed = 50;
    public virtual int GetCursorSpeed() //Can be overwritten by the faction script object
    {
        return BaseCursorSpeed;
    }

    //BUILDABLE:
    //can build civilian
    public bool BaseCivilianAble = true;
    //can build warrior
    public bool BaseWarriorAble = true;
    //can build hunter
    public bool BaseHunterAble = true;
    //can build siege
    public bool BaseSiegeAble = true;
    //can build farm
    public bool BaseFarmAble = true;
    //can build plantation
    public bool BasePlantationAble = true;
    //can build tower
    public bool BaseTowerAble = true;
    //can build barricade
    public bool BaseBarricadeAble = true;

    //COSTS:
    //civilian train cost
    public int BaseCivilianCost = 2;
    public virtual int GetCivilianCost() //Can be overwritten by the faction script object
    {
        return BaseCivilianCost;
    }
    //warrior train cost
    public int BaseWarriorCost = 2;
    public virtual int GetWarriorCost() //Can be overwritten by the faction script object
    {
        return BaseCivilianCost;
    }
    //hunter train cost
    public int BaseHunterCost = 2;
    public virtual int GetHunterCost() //Can be overwritten by the faction script object
    {
        return BaseCivilianCost;
    }
    //siege train cost
    public int BaseSiegeCost = 2;
    public virtual int GetSiegeCost() //Can be overwritten by the faction script object
    {
        return BaseCivilianCost;
    }

    //farm building cost
    public int BaseFarmCost = 2;
    public virtual int GetFarmCost() //Can be overwritten by the faction script object
    {
        return BaseFarmCost;
    }
    //plantation building cost
    public int BasePlantationCost = 2;
    public virtual int GetPlantationCost() //Can be overwritten by the faction script object
    {
        return BasePlantationCost;
    }
    //barricade building cost
    public int BaseBarricadeCost = 2;
    public virtual int GetBarricadeCost() //Can be overwritten by the faction script object
    {
        return BaseBarricadeCost;
    }
    //tower building cost
    public int BaseTowerCost = 2;
    public virtual int GetTowerCost() //Can be overwritten by the faction script object
    {
        return BaseTowerCost;
    }

    //UNIT STATS:
    //civilian health
    public int BaseCivilianHealth = 2;
    public virtual int GetCivilianHealth() //Can be overwritten by the faction script object
    {
        return BaseCivilianHealth;
    }
    //civilian sight
    public int BaseCivilianSight = 50;
    public virtual int GetCivilianSight() //Can be overwritten by the faction script object
    {
        return BaseCivilianSight;
    }
    //civilian range
    public int BaseCivilianRange = 1;
    public virtual int GetCivilianRange() //Can be overwritten by the faction script object
    {
        return BaseCivilianRange;
    }
    //civilian move speed
    public int BaseCivilianMoveSpeed = 5;
    public virtual int GetCivilianMoveSpeed() //Can be overwritten by the faction script object
    {
        return BaseCivilianMoveSpeed;
    }
    //civilian attack
    public int BaseCivilianAttack = 1;
    public virtual int GetCivilianAttack() //Can be overwritten by the faction script object
    {
        return BaseCivilianAttack;
    }
    //civilian attack speed
    public int BaseCivilianAttackSpeed = 2;
    public virtual int GetCivilianAttackSpeed() //Can be overwritten by the faction script object
    {
        return BaseCivilianAttackSpeed;
    }

    //warrior health
    public int BaseWarriorHealth = 3;
    public virtual int GetWarriorHealth() //Can be overwritten by the faction script object
    {
        return BaseWarriorHealth;
    }
    //warrior sight
    public int BaseWarriorSight = 25;
    public virtual int GetWarriorSight() //Can be overwritten by the faction script object
    {
        return BaseWarriorSight;
    }
    //warrior range
    public int BaseWarriorRange = 5;
    public virtual int GetWarriorRange() //Can be overwritten by the faction script object
    {
        return BaseWarriorRange;
    }
    //warrior move speed
    public int BaseWarriorMoveSpeed = 5;
    public virtual int GetWarriorMoveSpeed() //Can be overwritten by the faction script object
    {
        return BaseWarriorMoveSpeed;
    }
    //warrior attack
    public int BaseWarriorAttack = 2;
    public virtual int GetWarriorAttack() //Can be overwritten by the faction script object
    {
        return BaseWarriorAttack;
    }
    //warrior attack speed
    public int BaseWarriorAttackSpeed = 1;
    public virtual int GetWarriorAttackSpeed() //Can be overwritten by the faction script object
    {
        return BaseWarriorAttackSpeed;
    }

    //hunter health
    public int BaseHunterHealth = 2;
    public virtual int GetHunterHealth () //Can be overwritten by the faction script object
    {
        return BaseHunterHealth;
    }
    //hunter sight
    public int BaseHunterSight = 35;
    public virtual int GetHunterSight() //Can be overwritten by the faction script object
    {
        return BaseHunterSight;
    }
    //hunter range
    public int BaseHunterRange = 25;
    public virtual int GetHunterRange() //Can be overwritten by the faction script object
    {
        return BaseHunterRange;
    }
    //hunter move speed
    public int BaseHunterMoveSpeed = 6;
    public virtual int GetHunterMoveSpeed() //Can be overwritten by the faction script object
    {
        return BaseHunterMoveSpeed;
    }
    //hunter attack
    public int BaseHunterAttack = 1;
    public virtual int GetHunterAttack() //Can be overwritten by the faction script object
    {
        return BaseHunterAttack;
    }
    //hunter attack speed
    public int BaseHunterAttackSpeed = 2;
    public virtual int GetHunterAttackSpeed() //Can be overwritten by the faction script object
    {
        return BaseHunterAttackSpeed;
    }

    //siege health
    public int BaseSiegeHealth = 10;
    public virtual int GetSiegeHealth() //Can be overwritten by the faction script object
    {
        return BaseSiegeHealth;
    }
    //siege sight
    public int BaseSiegeSight = 25;
    public virtual int GetSiegeSight() //Can be overwritten by the faction script object
    {
        return BaseSiegeSight;
    }
    //siege range
    public int BaseSiegeRange = 5;
    public virtual int GetSiegeRange() //Can be overwritten by the faction script object
    {
        return BaseSiegeRange;
    }
    //siege move speed
    public int BaseSiegeMoveSpeed = 2;
    public virtual int GetSiegeMoveSpeed() //Can be overwritten by the faction script object
    {
        return BaseSiegeMoveSpeed;
    }
    //siege attack
    public int BaseSiegeAttack = 10;
    public virtual int GetSiegeAttack() //Can be overwritten by the faction script object
    {
        return BaseSiegeAttack;
    }
    //sieg
    //siege attack speed
    public int BaseSiegeAttackSpeed = 5;
    public virtual int GetSiegeAttackSpeed() //Can be overwritten by the faction script object
    {
        return BaseSiegeAttackSpeed;
    }

    //BUILDING STATS:
    //farm health
    public int BaseFarmHealth = 2;
    public virtual int GetFarmHealth() //Can be overwritten by the faction script object
    {
        return BaseFarmHealth;
    }
    //farm growth time
    public int BaseFarmGrowthTime = 10;
    public virtual int GetFarmGrowthTime() //Can be overwritten by the faction script object
    {
        return BaseFarmGrowthTime;
    }
    //farm resource amount
    public int BaseFarmResourceAmount = 1;
    public virtual int GetFarmResourceAmount() //Can be overwritten by the faction script object
    {
        return BaseFarmResourceAmount;
    }
    //farm resource type
    public string BaseFarmResourceType = "food";
    public virtual string GetFarmResourceType()
    {
        return BaseFarmResourceType;
    }

    //plantation health
    public int BasePlantationHealth = 2;
    public virtual int GetPlantationHealth() //Can be overwritten by the faction script object
    {
        return BasePlantationHealth;
    }
    //plantation growth time
    public int BasePlantationGrowthTime = 1;
    public virtual int GetPlantationGrowthTime() //Can be overwritten by the faction script object
    {
        return BasePlantationGrowthTime;
    }
    //plantation resource amount
    public int BasePlantationResourceAmount = 1;
    public virtual int GetPlantationResourceAmount() //Can be overwritten by the faction script object
    {
        return BasePlantationResourceAmount;
    }
    //plantation resource type
    public string BasePlantationResourceType = "wood";
    public virtual string GetPlantationResourceType()
    {
        return BasePlantationResourceType;
    }

    //barricade health
    public int BaseBarricadeHealth = 3;
    public virtual int GetBarricadeHealth()
    {
        return BaseBarricadeHealth;
    }

    //tower health
    public int BaseTowerHealth = 3;
    public virtual int GetTowerHealth()
    {
        return BaseTowerHealth;
    }
    //tower range
    public int BaseTowerRange = 25;
    public virtual int GetTowerRange()
    {
        return BaseTowerRange;
    }
    //tower attack
    public int BaseTowerAttack = 1;
    public virtual int GetTowerAttack()
    {
        return BaseTowerAttack;
    }
    //tower attack speed
    public int BaseTowerAttackSpeed = 1;
    public virtual int GetTowerAttackSpeed()
    {
        return BaseTowerAttackSpeed;
    }
}