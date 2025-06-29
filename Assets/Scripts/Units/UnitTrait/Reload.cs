using UnityEngine;
using System.Collections.Generic;

public class Reload : UnitTrait
{
    public Reload()
    {
        traitName = "Reload";
        unitTypes = new List<UnitType> { UnitType.Ranged, UnitType.RangedCavalry };
    }
    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        attacker.AddStatusEffect(new Reloading(1));
    }
}
