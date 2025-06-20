using UnityEngine;

public class 츄겐 : Unit
{
    private void Awake()
    {
        unitName = "츄겐";
        faction = Faction.Enemy;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 60,
            currentHealth = 60,
            attackPower = 50,
            defensePower = 50,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
