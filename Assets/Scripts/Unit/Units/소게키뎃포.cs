using UnityEngine;

public class 소게키뎃포 : Unit
{
    private void Awake()
    {
        unitName = "소게키 뎃포";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 20,
            currentHealth = 20,
            attackPower = 35,
            defensePower = 25,
            range = 4
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
