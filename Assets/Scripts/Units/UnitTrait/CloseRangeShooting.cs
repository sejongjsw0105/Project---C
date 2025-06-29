using UnityEngine;
using System.Collections.Generic;
public class CloseRangeShooting : UnitTrait
{
    public CloseRangeShooting()
    {
        traitName = "Close Range Shooting";
        unitTypes = new List<UnitType> { UnitType.Ranged, UnitType.RangedCavalry };
    }
    public override void OnApply(IUnit unit)
    {
        unit.state.CanAttack = true;
    }

}
