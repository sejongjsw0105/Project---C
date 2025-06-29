using UnityEngine;
using System.Collections.Generic;
public class CounterAttack : UnitTrait
{
    public CounterAttack()
    {
        traitName = "CounterAttack";
        unitTypes = new List<UnitType> { UnitType.Melee};

    }
    public override void OnAfterDamaged(IUnit from, IUnit target, DamageType damageType, int damage)
    {
        if (damageType == DamageType.Support && (from.unitType == UnitType.Melee || from.unitType ==UnitType.Cavalry))
        {
            AttackAction.Execute(from, target.area);
        }
    }
}
