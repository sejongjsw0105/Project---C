using UnityEngine;

public class 사무라이 : Unit
{
    private void Awake()
    {
        unitName = "사무라이";
        faction = Faction.Enemy;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 95,
            currentHealth = 95,
            attackPower = 45,
            defensePower = 35,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Fear>()
        };
    }
}
