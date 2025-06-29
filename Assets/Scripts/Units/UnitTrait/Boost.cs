using UnityEngine;
using System.Collections.Generic;

public class Boost : UnitTrait
{
    public int boostValue = 1; // 증가할 능력치 값
    public Boost()
    {
        traitName = "Boost";
        unitTypes = new List<UnitType>
        {
            UnitType.Melee,
            UnitType.Ranged,
            UnitType.RangedCavalry,
            UnitType.Cavalry
        };
    }
    public override void OnTurnEnd(IUnit from)
    {
        List<IUnit> units = GridHelper.Instance.GetAdjacentUnits(from);
        foreach (IUnit unit in units)
        {
            if (unit != null && unit.unitType == from.unitType)
            {
                unit.AddStatusEffect(new Boosted(1,20));   
            }
        }
    }

}
