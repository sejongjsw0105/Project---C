using UnityEngine;

public class 승자총통수 : Unit
{
    private void Awake()
    {
        unitName = "승자총통수";
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 25,
            currentHealth = 25,
            attackPower = 35,
            defensePower = 30,
            range = 2
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Reload>(),
            gameObject.AddComponent<Cover>()
        };
    }
}
