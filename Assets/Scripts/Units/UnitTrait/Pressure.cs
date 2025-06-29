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

    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        TryPressure(target);
    }

    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        Unit target = supporter.faction switch
        {
            Faction.Friendly => area.occupyingEnemyUnit,
            Faction.Enemy => area.occupyingFriendlyUnit,
            _ => null
        };

        TryPressure(target);
    }

    private void TryPressure(Unit target)
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
            Faction.Friendly => GridHelper.Instance.IsFriendlyMoveAllowed(target.area, retreatArea),
            Faction.Enemy => GridHelper.Instance.IsEnemyMoveAllowed(target.area, retreatArea),
            _ => false
        };

        if (!canMove) return;

        var moveContext = new MoveContext(target, retreatArea);
        moveContext.Resolve();

        Debug.Log($"[Pressure] {target.unitName}이 압박으로 인해 후퇴했습니다!");
    }
}
