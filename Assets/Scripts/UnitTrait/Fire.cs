using UnityEngine;
using System.Collections.Generic;
public class Fire : UnitTrait

{
    public Fire()
    {
        type = UnitTraitType.Fire;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry};
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        area.AddStatusEffect(new OnFire((int)(supporter.attackPower*0.5),4));
    }

}
