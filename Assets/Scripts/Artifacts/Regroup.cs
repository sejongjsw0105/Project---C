using UnityEngine;
using System.Collections.Generic;

public class Regroup : Artifact
{
    public List<UnitData> deadUnits = new();

    public override void OnDie(IUnit unit)
    {
        UnitData data = new UnitData
        {
            id = unit.unitId,
            name = unit.unitName,
            faction = Faction.Friendly, // 항상 아군화
            type = unit.unitType,
            currentHealth = unit.stats.maxHealth, // 최대 체력으로 복원
            baseStats = new UnitStats
            {
                maxHealth = unit.stats.maxHealth,
                attackPower = unit.stats.attackPower,
                defensePower = unit.stats.defensePower,
                range = unit.stats.range
            },
            upgradedStats = new UnitStats
            {
                maxHealth = unit.upgradedStats.maxHealth,
                attackPower = unit.upgradedStats.attackPower,
                defensePower = unit.upgradedStats.defensePower,
                range = unit.upgradedStats.range
            },
            isUpgraded = false,
            traits = new List<UnitTrait>(unit.unitTrait),
            statusEffects = new List<StatusEffect>() // 죽었을 때 상태이상은 유지하지 않음
        };

        deadUnits.Add(data);
    }

    public void RestoreUnits()
    {
        foreach (var data in deadUnits)
        {
            GameContext.Instance.myUnitDataList.Add(data); // 보유 목록에만 추가
        }

        deadUnits.Clear();
    }
}
