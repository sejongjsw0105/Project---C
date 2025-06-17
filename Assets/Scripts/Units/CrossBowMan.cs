using UnityEngine;
using static Archer;

public class CrossBowMan : Unit
{
    public CrossBowMan()
    {
        unitName = "CrossBowMan";
        unitId = 2;
        health = 80;
        attackPower = 20;
        defensePower = 5;
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;
    }




}
