using UnityEngine;
using System.Collections.Generic;

public class Boost : UnitTrait
{
    public int boostValue = 1; // 증가할 능력치 값
    public Boost()
    {
        type = UnitTraitType.Boost;

        unitTypes = new List<Unit.UnitType>
        {
            Unit.UnitType.Melee,
            Unit.UnitType.Ranged,
            Unit.UnitType.RangedCavalry,
            Unit.UnitType.Cavalry
        };
    }
    public override void OnUpdate(Unit from)
    {
        List<Unit> units = GridHelper.Instance.GetAdjacentUnits(from);
        foreach (Unit unit in units)
        {
            if (unit != null && unit.unitType == from.unitType)
            {
                unit.AddStatusEffect(new Boosted(1,20));
            }
        }
    }

}
