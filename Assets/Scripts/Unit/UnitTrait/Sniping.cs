using UnityEngine;
using System.Collections.Generic;
public class Sniping : UnitTrait
{
    public Sniping()
    {
        type = UnitTraitType.Sniping;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry};
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        switch (supporter.faction)
        {
            case Unit.Faction.Friendly:
                if (area.occupyingEnemyUnit != null)
                {
                    
                }
                break;
            case Unit.Faction.Enemy:
                if (area.occupyingFriendlyUnit != null)
                {
                    area.occupyingFriendlyUnit.AddStatusEffect(new Chaos()); ;
                }
                break;

        }

        
    }

}
