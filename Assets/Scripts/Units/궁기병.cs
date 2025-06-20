using UnityEngine;

public class 궁기병 : Unit
{
    private void Awake()
    {
        unitName = "궁기병";
        faction = Faction.Friendly;
        unitType = UnitType.RangedCavalry;

        stats = new UnitStats
        {
            maxHealth = 30,
            currentHealth = 30,
            attackPower = 20,
            defensePower = 10,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
