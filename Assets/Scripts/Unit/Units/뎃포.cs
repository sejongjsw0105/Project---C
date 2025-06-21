using UnityEngine;

public class 뎃포 : Unit
{
    private void Awake()
    {
        unitName = "뎃포";
        faction = Faction.Enemy;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 30,
            currentHealth = 30,
            attackPower = 20,
            defensePower = 40,
            range = 1
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<SuccesiveFiring>(),
            gameObject.AddComponent<Reload>()
        };
    }
}
