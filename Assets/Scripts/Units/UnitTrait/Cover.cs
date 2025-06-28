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
    public override ResultContext<int> OnBeforeSupport(Unit supporter, Area area, int value)
    {
        var result = new ResultContext<int>(value);

        Unit target = area.GetAllyOccupant(supporter);

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
