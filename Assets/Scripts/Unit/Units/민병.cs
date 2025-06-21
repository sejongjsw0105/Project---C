using UnityEngine;

public class 민병 : Unit
{
    private void Awake()
    {
        unitName = "민병";
        faction = Faction.Friendly;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 100,
            currentHealth = 100,
            attackPower = 40,
            defensePower = 35,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
