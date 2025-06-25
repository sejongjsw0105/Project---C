using System;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public static Unit CreateUnitFromData(UnitData data)
    {
        // 1. 유닛별 프리팹 로드
        GameObject prefab = Resources.Load<GameObject>($"Units/{data.name}");
        if (prefab == null)
        {
            Debug.LogError($"[UnitFactory] Resources/Units/{data.name}.prefab 을 찾을 수 없습니다.");
            return null;
        }

        // 2. 인스턴스 생성
        GameObject unitObj = GameObject.Instantiate(prefab);
        Unit unit = unitObj.GetComponent<Unit>();

        if (unit == null)
        {
            Debug.LogError($"[UnitFactory] {data.name} 프리팹에 Unit 컴포넌트가 없습니다.");
            return null;
        }

        // 3. 기본 데이터 주입
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

        // 5. 트레잇 초기화
        unit.unitTrait = new System.Collections.Generic.List<UnitTrait>();
        foreach (string traitName in data.traits)
        {
            UnitTrait trait = TraitFactory.CreateTrait(traitName, unit.gameObject);
            if (trait != null)
                unit.unitTrait.Add(trait);
        }

        // 6. 등록
        UnitManager.Instance.RegisterUnit(unit);
        return unit;
    }
}