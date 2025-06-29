using UnityEngine;
using System.Collections.Generic;

public class Sniping : UnitTrait
{
    public Sniping()
    {
        traitName = "Sniping";
        unitTypes = new List<UnitType> { UnitType.Ranged, UnitType.RangedCavalry };
    }

    public override void OnAfterSupport(IUnit supporter, IArea area, int value)
    {
        var target = area.GetEnemyOccupant(supporter);
        if (target != null)
        {
            target.AddStatusEffect(new Chaos());
        }
    }
}
