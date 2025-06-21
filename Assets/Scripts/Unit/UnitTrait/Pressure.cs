using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class Pressure : UnitTrait
{
    public Pressure()
    {
        type = UnitTraitType.Pressure;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry, Unit.UnitType.Ranged,Unit.UnitType.RangedCavalry };
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
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        Unit target;
        if (supporter.faction == Unit.Faction.Friendly)
        {
            target = area.occupyingEnemyUnit; // 지원 대상은 적 유닛
        }
        else if (supporter.faction == Unit.Faction.Enemy)
        {
            target = area.occupyingFriendlyUnit;
        }
        else
        {
            target = null; // 중립 유닛은 압박 대상이 아님
        }
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