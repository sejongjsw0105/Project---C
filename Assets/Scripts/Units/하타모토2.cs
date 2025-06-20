using UnityEngine;

public class 하타모토2 : Unit
{
    private void Awake()
    {
        unitName = "하타모토2";
        faction = Faction.Enemy;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 90,
            currentHealth = 90,
            attackPower = 30,
            defensePower = 30,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<PsychologicalWarfare>()
        };
    }
}
