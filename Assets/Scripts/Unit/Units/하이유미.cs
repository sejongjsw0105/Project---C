using UnityEngine;

public class 하이유미 : Unit
{
    private void Awake()
    {
        unitName = "하이 유미";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 20,
            currentHealth = 20,
            attackPower = 25,
            defensePower = 25,
            range = 3
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Fire>()
        };
    }
}
