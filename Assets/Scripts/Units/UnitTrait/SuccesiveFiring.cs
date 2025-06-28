using UnityEngine;
using System.Collections.Generic;
public class SuccesiveFiring : UnitTrait
{
    public SuccesiveFiring()
    {
        type = UnitTraitType.SuccesiveFiring;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Ranged };
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        Unit.Support support = supporter.GetSupport();
        support.DoSupport(area);
        BattleManager.Instance.RefundCommandPoints(BattleManager.Instance.GetActionCost(Action.Support));
        support.DoSupport(area);
        BattleManager.Instance.RefundCommandPoints(BattleManager.Instance.GetActionCost(Action.Support));
    }
}