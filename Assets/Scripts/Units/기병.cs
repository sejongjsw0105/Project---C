using UnityEngine;

public class 기병 : Unit
{
    private void Awake()
    {
        unitName = "기병";
        faction = Faction.Enemy;
        unitType = UnitType.Cavalry;

        stats = new UnitStats
        {
            maxHealth = 50,
            currentHealth = 50,
            attackPower = 30,
            defensePower = 10,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
