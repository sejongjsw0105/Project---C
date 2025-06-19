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
        if (support == null) return; // ������ ������ ����
        support.validConditions = new List<AreaCondition>{ AreaCondition.FriendlyOccupied, AreaCondition.InCombat }; // ���� ���� ���ǿ� �� ���� ���� �߰�
        support.validFactions = new List<Unit.Faction> { Unit.Faction.Friendly }; // ���� ���� ���� ����
    }
    public override (int, bool) OnBeforeSupport(Unit supporter, Area area, int value)
    {
        switch(supporter.faction)
        {
            case Unit.Faction.Friendly:
                if (area.occupyingFriendlyUnit == null) return (0, false); // ���� ����� ���ų� �����ڰ� �ƴ� ���
                area.occupyingFriendlyUnit.AddStatusEffect(new Covered(supporter)); // Covering ȿ���� 1�� ���� ����
                return (0, false);
            case Unit.Faction.Enemy:
                if (area.occupyingEnemyUnit == null) return (0, false); // ���� ����� ���ų� �����ڰ� �ƴ� ���
                area.occupyingEnemyUnit.AddStatusEffect(new Covered(supporter)); // Covering ȿ���� 1�� ���� ����
                return (0, false);
        }
        return (0, false); // ������ �����ϸ� ���� �� ��ȯ

    }
}
