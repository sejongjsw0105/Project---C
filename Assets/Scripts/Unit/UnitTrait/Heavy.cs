using UnityEngine;
using System.Collections.Generic;
public class Heavy : UnitTrait
    {
    bool isAttacked = false;
    bool isMoved = false;
    public Heavy()
    {
        type = UnitTraitType.Heavy;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged, Unit.UnitType.RangedCavalry, Unit.UnitType.Cavalry, Unit.UnitType.Melee };
    }
    public override (int, bool) OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        if (isMoved) { return (0, false); }
        return (damage, true);
    }
    public override (int, bool) OnBeforeSupport(Unit supporter, Area area, int value)
    {
        if (isMoved) { return (0, false); }
        return (value, true);
    }
    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        isAttacked = true;
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        isAttacked = true;
    }
    public override void OnUpdate(Unit from)
    {
        isAttacked = false;
    }
    public override (int, bool) OnBeforeMove(Unit unit, Area target, int moveRange)
    {
        if (isAttacked) { return (0, false); }
        return (moveRange, true);
    }
    public override void OnAfterMove(Unit unit, Area target)
    {
        isMoved = true;
    }

}
