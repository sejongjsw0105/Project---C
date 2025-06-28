using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Regroup : Artifact
{
    public List<UnitData> deadUnits = new();

    public override void OnDie(Unit unit)
    {
        UnitData data = new UnitData
        {
            id = unit.unitId,
            name = unit.unitName,
            faction = "Friendly", // �׻� �Ʊ�ȭ
            type = unit.unitType.ToString(),
            maxHealth = unit.stats.maxHealth,
            attack = unit.stats.attackPower,
            defense = unit.stats.defensePower,
            range = unit.stats.range,
            traits = unit.unitTrait,
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
