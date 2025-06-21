using UnityEngine;

public class 대조총병 : Unit
{
    private void Awake()
    {
        unitName = "대조총병";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 30,
            currentHealth = 30,
            attackPower = 55,
            defensePower = 25,
            range = 2
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Pressure>(),
            gameObject.AddComponent<Reload>(),
            gameObject.AddComponent<Heavy>()
        };
    }
}
