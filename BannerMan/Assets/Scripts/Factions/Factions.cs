public enum Factions
{
    CatCoalition = 0,
    ElephantElders = 1,
    SlothKingdom = 2,
    AlligatorEmpire = 3,
}

public static class FactionHelper
{
    public static Faction GetFaction(Factions faction)
    {
        switch(faction)
        {
            case Factions.CatCoalition:
                return new TheCatCoalition();
            case Factions.ElephantElders:
                return new TheElephantElders();
            case Factions.SlothKingdom:
                return new TheSlothKingdom();
            case Factions.AlligatorEmpire:
                return new TheAlligatorEmpire();
            default:
                return null;
        }
    }

}