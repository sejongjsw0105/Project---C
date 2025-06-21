using UnityEngine;

public class 보즈 : Unit
{
    private void Awake()
    {
        unitName = "보즈";
        faction = Faction.Enemy;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 70,
            currentHealth = 70,
            attackPower = 10,
            defensePower = 30,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            //gameObject.AddComponent<Healer>()
        };
    }
}
