using UnityEngine;

public class 사수 : Unit
{
    private void Awake()
    {
        unitName = "사수";
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 20,
            currentHealth = 20,
            attackPower = 30,
            defensePower = 35,
            range = 2
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
