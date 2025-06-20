using UnityEngine;

public class 기마갑사 : Unit
{
    private void Awake()
    {
        unitName = "기마갑사";
        faction = Faction.Friendly;
        unitType = UnitType.RangedCavalry;

        stats = new UnitStats
        {
            maxHealth = 25,
            currentHealth = 25,
            attackPower = 20,
            defensePower = 10,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<CloseRangeShooting>()
        };
    }
}
