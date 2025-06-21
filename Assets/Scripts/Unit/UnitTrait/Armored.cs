using UnityEngine;
using System.Collections.Generic;

public class Armored : UnitTrait
{
    public Armored()
    {
        type = UnitTraitType.Armored;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };
    }
    public override void OnUpdate(Unit from, Area area)
    {
        from.AddStatusEffect(new Protected());
    }
}
