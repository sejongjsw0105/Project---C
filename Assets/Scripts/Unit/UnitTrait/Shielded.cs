using UnityEngine;
using System.Collections.Generic;

public class Shielded : UnitTrait
{
    public Shielded()
    {
        type = UnitTraitType.Shielded;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry};
    }
    public override (int, bool) OnBeforeDamaged(Unit from, Unit target, Unit.DamageType damageType, int damage)
    {
        if(damageType == Unit.DamageType.Support && (from.unitType == Unit.UnitType.Ranged && from.unitType == Unit.UnitType.RangedCavalry))
        {
            return ((int)(damage / 2), true);
        }
        return (damage, true);
    }
}
