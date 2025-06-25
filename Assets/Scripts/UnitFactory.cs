using System;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public static Unit CreateUnitFromData(UnitData data)
    {
        // 1. ���ֺ� ������ �ε�
        GameObject prefab = Resources.Load<GameObject>($"Units/{data.name}");
        if (prefab == null)
        {
            Debug.LogError($"[UnitFactory] Resources/Units/{data.name}.prefab �� ã�� �� �����ϴ�.");
            return null;
        }

        // 2. �ν��Ͻ� ����
        GameObject unitObj = GameObject.Instantiate(prefab);
        Unit unit = unitObj.GetComponent<Unit>();

        if (unit == null)
        {
            Debug.LogError($"[UnitFactory] {data.name} �����տ� Unit ������Ʈ�� �����ϴ�.");
            return null;
        }

        // 3. �⺻ ������ ����
        unit.unitId = data.id;
        unit.unitName = data.name;
        unit.faction = Enum.Parse<Unit.Faction>(data.faction);
        unit.unitType = Enum.Parse<Unit.UnitType>(data.type);
        unit.isUpgraded = data.isUpgraded;
        unit.health = data.currentHealth;

        unit.stats = new Unit.UnitStats
        {
            maxHealth = data.maxHealth,
            attackPower = data.attack,
            defensePower = data.defense,
            range = data.range
        };

        unit.upgradedStats = new Unit.UnitStats
        {
            maxHealth = data.maxHealth,
            attackPower = data.attack,
            defensePower = data.defense,
            range = data.range
        };

        if (data.isUpgraded)
        {
            unit.attackPower = unit.upgradedStats.attackPower;
            unit.defensePower = unit.upgradedStats.defensePower;
            unit.range = unit.upgradedStats.range;
        }
        else
        {
            unit.attackPower = unit.stats.attackPower;
            unit.defensePower = unit.stats.defensePower;
            unit.range = unit.stats.range;
        }

        // 5. Ʈ���� �ʱ�ȭ
        unit.unitTrait = new System.Collections.Generic.List<UnitTrait>();
        foreach (string traitName in data.traits)
        {
            UnitTrait trait = TraitFactory.CreateTrait(traitName, unit.gameObject);
            if (trait != null)
                unit.unitTrait.Add(trait);
        }

        // 6. ���
        UnitManager.Instance.RegisterUnit(unit);
        return unit;
    }
}