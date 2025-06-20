using UnityEngine;

public class 킨세츠뎃포 : Unit
{
    private void Awake()
    {
        unitName = "킨세츠 뎃포";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 25,
            currentHealth = 25,
            attackPower = 30,
            defensePower = 25,
            range = 2
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<Cover>()
        };
    }
}
