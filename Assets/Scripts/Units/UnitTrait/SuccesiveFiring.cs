using UnityEngine;
using System.Collections.Generic;

public class SuccesiveFiring : UnitTrait
{
    public SuccesiveFiring()
    {
        traitName = "Succesive Firing";
        unitTypes = new List<UnitType> { UnitType.Ranged };
    }

    public override void OnAfterSupport(IUnit supporter, IArea area, int value)
    {
        var target = area.GetEnemyOccupant(supporter);
        if (target == null) return;

        for (int i = 0; i < 2; i++)
        {
            DamageAction.Execute(supporter, target, DamageType.Support, value);
        }
    }
}