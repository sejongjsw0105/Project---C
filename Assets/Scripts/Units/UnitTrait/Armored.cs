using UnityEngine;
using System.Collections.Generic;

public class Armored : UnitTrait
{
    public Armored()
    {
        traitName = "Armored";
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry };
    }
    public override void OnTurnEnd(IUnit from)
    {
        from.AddStatusEffect(new Protected());
    }
}
