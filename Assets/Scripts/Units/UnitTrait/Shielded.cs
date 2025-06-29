using UnityEngine;
using System.Collections.Generic;

public class Shielded : UnitTrait
{
    public Shielded()
    {
        traitName = "Shielded";
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry};
    }
    public override ResultContext<int> OnBeforeDamaged(IUnit from, IUnit target, DamageType damageType, int damage)
    {
        var context = new ResultContext<int>(damage);
        if (damageType == DamageType.Support && (from.unitType == UnitType.Ranged && from.unitType == UnitType.RangedCavalry))
        {
            context.Modify(damage / 2);
            return context;
        }
        return context;
    }
}
