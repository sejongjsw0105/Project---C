using UnityEngine;

public class 완구포대 : Unit
{
    private void Awake()
    {
        unitName = "완구포대";
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;

        stats = new UnitStats
        {
            maxHealth = 30,
            currentHealth = 30,
            attackPower = 55,
            defensePower = 30,
            range = 2
        };

        health = stats.maxHealth;
        attackPower = stats.attackPower;
        defensePower = stats.defensePower;
        range = stats.range;
        unitTrait = new System.Collections.Generic.List<UnitTrait>
        {
            gameObject.AddComponent<LongReload>(),
            //gameObject.AddComponent<AreaAttack>(),
            gameObject.AddComponent<Heavy>()
        };
    }
}
