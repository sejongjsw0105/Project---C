using UnityEngine;
using System.Collections.Generic;

public class Heavy : UnitTrait
{
    private bool hasMoved = false;
    private bool hasAttacked = false;

    public Heavy()
    {
        traitName = "Heavy";
        unitTypes = new List<UnitType>
        {
            UnitType.Melee,
            UnitType.Cavalry,
            UnitType.Ranged,
            UnitType.RangedCavalry
        };
    }

    public override ResultContext<int> OnBeforeAttack(Unit attacker, Unit target, int damage)
    {
        var ctx = new ResultContext<int>(damage);
        if (hasMoved)
            ctx.Block("Heavy");
        return ctx;
    }

    public override ResultContext<int> OnBeforeSupport(Unit supporter, Area area, int value)
    {
        var ctx = new ResultContext<int>(value);
        if (hasMoved)
            ctx.Block("Heavy");
        return ctx;
    }

    public override ResultContext<int> OnBeforeMove(Unit unit, Area target, int moveRange)
    {
        var ctx = new ResultContext<int>(moveRange);
        if (hasAttacked)
            ctx.Block("Heavy");
        return ctx;
    }

    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        hasAttacked = true;
    }

    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        hasAttacked = true;
    }

    public override void OnAfterMove(Unit unit, Area target)
    {
        hasMoved = true;
    }

    public override void OnTurnEnd(Unit unit)
    {
        hasMoved = false;
        hasAttacked = false;
    }
}
