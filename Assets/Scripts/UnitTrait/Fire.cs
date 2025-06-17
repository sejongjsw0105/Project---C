using UnityEngine;
using System.Collections.Generic;
public class Fire : UnitTrait

{
    public Fire()
    {
        type = UnitTraitType.Fire;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry, Unit.UnitType.Melee };
        TraitId = 1; // ���÷� 1�� Trait ID�� ���
    }
    public override int OnBeforeSupport(Unit supporter, Area area)
    {
        area.AddStatusEffect(new OnFire(supporter.attackPower,1));
        return base.OnBeforeSupport(supporter, area);
    }

}
