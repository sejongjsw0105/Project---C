using System;
using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    public static Unit CreateUnitFromData(UnitData data)
    {
        // 1. 프리팹 로드
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
        unit.faction = data.faction;
        unit.unitType = data.type;
        unit.currentHealth = data.currentHealth;
        unit.unitData = data;

        // 4. 스탯 적용 (업그레이드 여부 반영)
        UnitStats selectedStats = data.isUpgraded ? data.upgradedStats : data.baseStats;
        unit.stats = new UnitStats
        {
            maxHealth = selectedStats.maxHealth,
            attackPower = selectedStats.attackPower,
            defensePower = selectedStats.defensePower,
            range = selectedStats.range
        };

        // 5. 트레잇 복원
        unit.unitTrait = new System.Collections.Generic.List<UnitTrait>();
        foreach (var trait in data.traits)
        {
            if (trait != null)
                unit.unitTrait.Add(trait); // 이미 MonoBehaviour 컴포넌트로 존재한다고 가정
        }

        // 6. 상태이상 복원 (선택 사항)
        unit.statusEffects = new System.Collections.Generic.List<StatusEffect>();
        foreach (var effect in data.statusEffects)
        {
            if (effect != null)
                unit.statusEffects.Add(effect); // 상태이상 유지
        }

        // 7. 등록
        UnitManager.Instance.RegisterUnit(unit);
        return unit;
    }
}
