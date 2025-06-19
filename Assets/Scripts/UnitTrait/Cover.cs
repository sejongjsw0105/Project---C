using System.Collections.Generic;
using UnityEngine;

public class Cover : UnitTrait
{
    public Cover()
    {
        type = UnitTraitType.Cover;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry};

    }
    public override void OnApply(Unit unit)
    {
        Unit.Support support= unit.GetSupport();
        if (support == null) return; // 지원이 없으면 종료
        support.validConditions = new List<AreaCondition>{ AreaCondition.FriendlyOccupied, AreaCondition.InCombat }; // 지원 가능 조건에 적 점령 영역 추가
        support.validFactions = new List<Unit.Faction> { Unit.Faction.Friendly }; // 지원 가능 진영 설정
    }
    public override (int, bool) OnBeforeSupport(Unit supporter, Area area, int value)
    {
        switch(supporter.faction)
        {
            case Unit.Faction.Friendly:
                if (area.occupyingFriendlyUnit == null) return (0, false); // 지원 대상이 없거나 지원자가 아닌 경우
                area.occupyingFriendlyUnit.AddStatusEffect(new Covered(supporter)); // Covering 효과를 1턴 동안 적용
                return (0, false);
            case Unit.Faction.Enemy:
                if (area.occupyingEnemyUnit == null) return (0, false); // 지원 대상이 없거나 지원자가 아닌 경우
                area.occupyingEnemyUnit.AddStatusEffect(new Covered(supporter)); // Covering 효과를 1턴 동안 적용
                return (0, false);
        }
        return (0, false); // 지원이 가능하면 원래 값 반환

    }
}
