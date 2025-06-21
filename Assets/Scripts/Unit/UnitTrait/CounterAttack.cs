using UnityEngine;
using System.Collections.Generic;
public class CounterAttack : UnitTrait
{
    public CounterAttack()
    {
        type = UnitTraitType.CounterAttack;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee};

    }
    public override void OnAfterDamaged(Unit from, Unit target, Unit.DamageType damageType, int damage)
    {
        if (damageType == Unit.DamageType.Support && (from.unitType == Unit.UnitType.Melee || from.unitType ==Unit.UnitType.Cavalry))
        {
            target.DoAttack(from); // 반격을 수행
        }
    }
}
