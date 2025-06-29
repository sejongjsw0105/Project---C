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

    public override ResultContext<int> OnBeforeAttack(IUnit attacker, IUnit target, int damage)
    {
        var ctx = new ResultContext<int>(damage);
        if (hasMoved)
            ctx.Block("Heavy");
        return ctx;
    }

    public override ResultContext<int> OnBeforeSupport(IUnit supporter, IArea area, int value)
    {
        var ctx = new ResultContext<int>(value);
        if (hasMoved)
            ctx.Block("Heavy");
        return ctx;
    }

    public override ResultContext<int> OnBeforeMove(IUnit unit, IArea target, int moveRange)
    {
        var ctx = new ResultContext<int>(moveRange);
        if (hasAttacked)
            ctx.Block("Heavy");
        return ctx;
    }

    public override void OnAfterAttack(IUnit attacker, IUnit target, int damage)
    {
        hasAttacked = true;
    }

    public override void OnAfterSupport(IUnit supporter, IArea area, int value)
    {
        hasAttacked = true;
    }

    public override void OnAfterMove(IUnit unit, IArea target)
    {
        hasMoved = true;
    }

    public override void OnTurnEnd(IUnit unit)
    {
        hasMoved = false;
        hasAttacked = false;
    }
}
