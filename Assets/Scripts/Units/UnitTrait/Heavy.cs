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
        if (hasMoved)
            return new ResultContext<int>(0).Block("Heavy");
        return new ResultContext<int>(damage);
    }

    public override ResultContext<int> OnBeforeSupport(Unit supporter, Area area, int value)
    {
        if (hasMoved)
            return new ResultContext<int>(0).Block("Heavy");
        return new ResultContext<int>(value);
    }

    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        hasAttacked = true;
    }

    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        hasAttacked = true;
    }

    public override ResultContext<int> OnBeforeMove(Unit unit, Area target, int moveRange)
    {
        if (hasAttacked)
            return new ResultContext<int>(0).Block("Heavy");
        return new ResultContext<int>(moveRange);
    }

    public override void OnAfterMove(Unit unit, Area target)
    {
        hasMoved = true;
    }

    public override void OnTurnEnd(Unit unit)
    {
        // 턴 종료 시 초기화
        hasMoved = false;
        hasAttacked = false;
    }
}
