using UnityEngine;

public class 팽배수 : Unit
{
    private void Awake()
    {
        unitName = "팽배수";
        faction = Faction.Friendly;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 110,
            currentHealth = 110,
            attackPower = 35,
            defensePower = 50,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Shielded>()
        };
    }
}
