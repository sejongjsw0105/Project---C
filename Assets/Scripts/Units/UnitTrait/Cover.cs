using System.Collections.Generic;
using UnityEngine;

public class Cover : UnitTrait
{
    public Cover()
    {
        traitName = "Cover";
        unitTypes = new List<UnitType>
        {
            UnitType.Melee,
            UnitType.Cavalry,
            UnitType.Ranged,
            UnitType.RangedCavalry
        };
    }
    public override ResultContext<int> OnBeforeSupport(IUnit supporter, IArea area, int value)
    {
        var result = new ResultContext<int>(value);

        IUnit target = area.GetAllyOccupant(supporter);

        if (target == null)
        {
            result.Block("Cover");
            return result;
        }

        target.AddStatusEffect(new Covered(supporter));
        result.Value = 0;
        result.Block("Cover");

        return result;
    }

}
