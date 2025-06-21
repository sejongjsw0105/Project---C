using UnityEngine;
using System.Collections.Generic;
public class PsychologicalWarfare : UnitTrait
{
    Unit targetUnit;
    public PsychologicalWarfare()
    {
        type = UnitTraitType.PsychologicalWarfare;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        targetUnit = target;
        return (damage, true);
    }
    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        if (target == null)
        {
            Unit reviveUnit = targetUnit;
            targetUnit.faction = attacker.faction;
            targetUnit.AddStatusEffect(new Defeated()); 
        }
    }
}