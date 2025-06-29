using UnityEngine;
using System.Collections.Generic;

public class Regroup : Artifact
{
    public List<UnitData> deadUnits = new();

    public override void OnDie(Unit unit)
    {
        UnitData data = new UnitData
        {
            id = unit.unitId,
            name = unit.unitName,
            faction = Faction.Friendly, // �׻� �Ʊ�ȭ
            type = unit.unitType,
            currentHealth = unit.stats.maxHealth, // �ִ� ü������ ����
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
            statusEffects = new List<StatusEffect>() // �׾��� �� �����̻��� �������� ����
        };

        deadUnits.Add(data);
    }

    public void RestoreUnits()
    {
        foreach (var data in deadUnits)
        {
            GameContext.Instance.myUnitDataList.Add(data); // ���� ��Ͽ��� �߰�
        }

        deadUnits.Clear();
    }
}
