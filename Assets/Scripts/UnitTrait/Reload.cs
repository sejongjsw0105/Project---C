using UnityEngine;
using System.Collections.Generic;

public class Reload : UnitTrait
{
    public Reload()
    {
        type = UnitTraitType.Reload;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry };
        TraitId = 2; // ���÷� 2�� Trait ID�� ���
    }
    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        attacker.AddStatusEffect(new Reloading(1));
    }
}
