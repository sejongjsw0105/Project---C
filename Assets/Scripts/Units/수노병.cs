using UnityEngine;

public class 수노병 : Unit
{
    private void Awake()
    {
        unitName = "수노병";
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 25,
            currentHealth = 25,
            attackPower = 55,
            defensePower = 35,
            range = 2
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Reload>()
        };
    }
}
