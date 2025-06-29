using System.Collections.Generic;
using UnityEngine;

public class Pressure : UnitTrait
{
    public Pressure()
    {
        traitName = "Pressure";
        unitTypes = new List<UnitType>
        {
            UnitType.Melee,
            UnitType.Cavalry,
            UnitType.Ranged,
            UnitType.RangedCavalry
        };
    }

    public override void OnAfterAttack(IUnit attacker, IUnit target, int damage)
    {
        TryPressure(target);
    }

    public override void OnAfterSupport(IUnit supporter, IArea area, int value)
    {
        IUnit target = supporter.faction switch
        {
            Faction.Friendly => area.occupyingEnemyUnit,
            Faction.Enemy => area.occupyingFriendlyUnit,
            _ => null
        };

        TryPressure(target);
    }

    private void TryPressure(IUnit target)
    {
        if (target == null || target.stats == null || target.area == null) return;

        if (target.currentHealth > target.stats.maxHealth / 2) return;

        int x = target.area.areaIndexX;
        int y = target.area.areaIndexY;

        int retreatY = target.faction == Faction.Friendly ? y - 1 : y + 1;
        Area retreatArea = AreaManager.Instance.GetArea(x, retreatY);

        if (retreatArea == null) return;

        bool canMove = target.faction switch
        {
            Faction.Friendly => GridHelper.Instance.IsMoveAllowed(target, retreatArea),
            Faction.Enemy => GridHelper.Instance.IsMoveAllowed(target, retreatArea),
            _ => false
        };

        if (!canMove) return;

        var moveContext = new MoveContext(target, retreatArea);
        moveContext.Resolve();
    }
}
