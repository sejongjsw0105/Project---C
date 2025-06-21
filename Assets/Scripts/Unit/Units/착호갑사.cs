using UnityEngine;

public class 착호갑사 : Unit
{
    private void Awake()
    {
        unitName = "착호 갑사";
        faction = Faction.Friendly;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 95,
            currentHealth = 95,
            attackPower = 60,
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
