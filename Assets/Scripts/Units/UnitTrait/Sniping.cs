using UnityEngine;
using System.Collections.Generic;
public class Sniping : UnitTrait
{
    public Sniping()
    {
        type = UnitTraitType.Sniping;
        unitTypes = new List<UnitType> { UnitType.Ranged, UnitType.RangedCavalry};
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        switch (supporter.faction)
        {
            case Faction.Friendly:
                if (area.occupyingEnemyUnit != null)
                {
                    
                }
                break;
            case Faction.Enemy:
                if (area.occupyingFriendlyUnit != null)
                {
                    area.occupyingFriendlyUnit.AddStatusEffect(new Chaos()); ;
                }
                break;

        }

        
    }

}
