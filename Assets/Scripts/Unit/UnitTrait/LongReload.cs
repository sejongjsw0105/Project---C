using UnityEngine;
using System.Collections.Generic;

public class LongReload : UnitTrait
{
    public LongReload()
    {
        type = UnitTraitType.Reload;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry };
    }
    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        attacker.AddStatusEffect(new Reloading(2));
    }
}
