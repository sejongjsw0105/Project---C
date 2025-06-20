using UnityEngine;

public class 지원궁수 : Unit
{
    private void Awake()
    {
        unitName = "지원궁수";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 20,
            currentHealth = 20,
            attackPower = 15,
            defensePower = 30,
            range = 3
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Cover>()
        };
    }
}
