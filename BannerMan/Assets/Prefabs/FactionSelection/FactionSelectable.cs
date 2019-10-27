using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactionSelectable : MonoBehaviour
{
    public Factions myFaction;
    private Faction faction;
    public Text myName;

    private void Start()
    {
        faction = FactionHelper.GetFaction(myFaction);
        myName.text = faction.GetName();
    }
}
