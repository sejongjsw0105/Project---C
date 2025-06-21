using UnityEngine;

public class 살수 : Unit
{
    private void Awake()
    {
        unitName = "살수";
        faction = Faction.Friendly;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 90,
            currentHealth = 90,
            attackPower = 40,
            defensePower = 35,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<CounterAttack>()
        };
    }
}
