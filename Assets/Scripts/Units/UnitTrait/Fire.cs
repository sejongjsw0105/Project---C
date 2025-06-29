using UnityEngine;
using System.Collections.Generic;
public class Fire : UnitTrait

{
    public Fire()
    {
        traitName = "Fire";
        unitTypes = new List<UnitType> { UnitType.Ranged, UnitType.RangedCavalry};
    }
    public override void OnAfterSupport(IUnit supporter, IArea area, int value)
    {
        area.AddStatusEffect(new OnFire((int)(supporter.stats.attackPower*0.5),4));
    }

}
