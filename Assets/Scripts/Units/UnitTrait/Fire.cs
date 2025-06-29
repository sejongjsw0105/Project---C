using UnityEngine;
using System.Collections.Generic;
public class Fire : UnitTrait

{
    public Fire()
    {
        traitName = "Fire";
        unitTypes = new List<UnitType> { UnitType.Ranged, UnitType.RangedCavalry};
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        area.AddStatusEffect(new OnFire((int)(supporter.stats.attackPower*0.5),4));
    }

}
