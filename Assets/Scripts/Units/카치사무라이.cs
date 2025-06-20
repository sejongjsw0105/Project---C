using UnityEngine;

public class 카치사무라이 : Unit
{
    private void Awake()
    {
        unitName = "카치 사무라이";
        faction = Faction.Enemy;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 100,
            currentHealth = 100,
            attackPower = 50,
            defensePower = 40,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
    }
}
