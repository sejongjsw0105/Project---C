using UnityEngine;
using System.Collections.Generic;
public class CloseRangeShooting : UnitTrait
{
    public CloseRangeShooting()
    {
        type = UnitTraitType.CloseRangeShooting;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry };
    }
    public override void OnApply(Unit unit)
    {
        unit.isAttackable = true; // �ٰŸ� ����� �����ϵ��� ����   
    }

}
