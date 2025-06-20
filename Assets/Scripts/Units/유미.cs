using UnityEngine;

public class 유미 : Unit
{
    private void Awake()
    {
        unitName = "유미";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 20,
            currentHealth = 20,
            attackPower = 20,
            defensePower = 25,
            range = 3
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
