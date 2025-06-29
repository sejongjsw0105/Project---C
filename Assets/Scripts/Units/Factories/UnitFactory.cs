using System;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public static Unit CreateUnitFromData(UnitData data)
    {
        // 1. ������ �ε�
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
        unit.faction = data.faction;
        unit.unitType = data.type;
        unit.currentHealth = data.currentHealth;
        unit.unitData = data;

        // 4. ���� ���� (���׷��̵� ���� �ݿ�)
        UnitStats selectedStats = data.isUpgraded ? data.upgradedStats : data.baseStats;
        unit.stats = new UnitStats
        {
            maxHealth = selectedStats.maxHealth,
            attackPower = selectedStats.attackPower,
            defensePower = selectedStats.defensePower,
            range = selectedStats.range
        };

        // 5. Ʈ���� ����
        unit.unitTrait = new System.Collections.Generic.List<UnitTrait>();
        foreach (var trait in data.traits)
        {
            if (trait != null)
                unit.unitTrait.Add(trait); // �̹� MonoBehaviour ������Ʈ�� �����Ѵٰ� ����
        }

        // 6. �����̻� ���� (���� ����)
        unit.statusEffects = new System.Collections.Generic.List<StatusEffect>();
        foreach (var effect in data.statusEffects)
        {
            if (effect != null)
                unit.statusEffects.Add(effect); // �����̻� ����
        }

        // 7. ���
        UnitManager.Instance.RegisterUnit(unit);
        return unit;
    }
}
