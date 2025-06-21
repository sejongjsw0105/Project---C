using UnityEngine;

public class 시루도사무라이 : Unit
{
    private void Awake()
    {
        unitName = "시루도 사무라이";
        faction = Faction.Enemy;
        unitType = UnitType.Melee;

        stats = new UnitStats
        {
            maxHealth = 110,
            currentHealth = 110,
            attackPower = 30,
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
