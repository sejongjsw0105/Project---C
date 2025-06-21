using UnityEngine;

public class 장궁병 : Unit
{
    private void Awake()
    {
        unitName = "장궁병";
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 20,
            currentHealth = 20,
            attackPower = 15,
            defensePower = 10,
            range = 3
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Sniping>()
        };
    }
}
