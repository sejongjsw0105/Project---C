using UnityEngine;

public class 타테유미 : Unit
{
    private void Awake()
    {
        unitName = "타테유미";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 30,
            currentHealth = 30,
            attackPower = 20,
            defensePower = 40,
            range = 2
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
