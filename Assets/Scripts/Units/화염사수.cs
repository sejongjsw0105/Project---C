using UnityEngine;

public class 화염사수 : Unit
{
    private void Awake()
    {
        unitName = "화염사수";
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 20,
            currentHealth = 20,
            attackPower = 25,
            defensePower = 35,
            range = 2
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
