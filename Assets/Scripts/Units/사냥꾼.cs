using UnityEngine;

public class 사냥꾼 : Unit
{
    private void Awake()
    {
        unitName = "사냥꾼";
        faction = Faction.Friendly;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 80,
            currentHealth = 80,
            attackPower = 45,
            defensePower = 30,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Surprise>()
        };
    }
}
