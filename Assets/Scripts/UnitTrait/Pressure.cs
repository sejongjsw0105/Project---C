using UnityEngine;
using System.Collections.Generic;

public class Pressure : UnitTrait
{
    public Pressure()
    {
        type = UnitTraitType.Pressure;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };
    }

    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        if (target == null || target.stats == null) return;

        if (target.health > target.stats.maxHealth / 2) return; // 압박 조건: 체력 절반 이하

        int x = target.area.areaIndexX;
        int y = target.area.areaIndexY;

        if (target.faction == Unit.Faction.Friendly)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y - 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea); // 후퇴 이동
                Debug.Log($"[Pressure] {target.unitName}이 압박으로 인해 후퇴합니다!");
            }
        }
        else if (target.faction == Unit.Faction.Enemy)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y + 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea);
                Debug.Log($"[Pressure] {target.unitName}이 압박으로 인해 후퇴합니다!");
            }
        }
    }
}