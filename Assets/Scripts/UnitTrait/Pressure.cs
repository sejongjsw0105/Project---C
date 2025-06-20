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

        if (target.health > target.stats.maxHealth / 2) return; // �й� ����: ü�� ���� ����

        int x = target.area.areaIndexX;
        int y = target.area.areaIndexY;

        if (target.faction == Unit.Faction.Friendly)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y - 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea); // ���� �̵�
                Debug.Log($"[Pressure] {target.unitName}�� �й����� ���� �����մϴ�!");
            }
        }
        else if (target.faction == Unit.Faction.Enemy)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y + 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea);
                Debug.Log($"[Pressure] {target.unitName}�� �й����� ���� �����մϴ�!");
            }
        }
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        Unit target;
        if (supporter.faction == Unit.Faction.Friendly)
        {
            target = area.occupyingEnemyUnit; // ���� ����� �� ����
        }
        else if (supporter.faction == Unit.Faction.Enemy)
        {
            target = area.occupyingFriendlyUnit;
        }
        else
        {
            target = null; // �߸� ������ �й� ����� �ƴ�
        }
        if (target == null || target.stats == null) return;

        if (target.health > target.stats.maxHealth / 2) return; // �й� ����: ü�� ���� ����

        int x = target.area.areaIndexX;
        int y = target.area.areaIndexY;

        if (target.faction == Unit.Faction.Friendly)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y - 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea); // ���� �̵�
                Debug.Log($"[Pressure] {target.unitName}�� �й����� ���� �����մϴ�!");
            }
        }
        else if (target.faction == Unit.Faction.Enemy)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y + 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea);
                Debug.Log($"[Pressure] {target.unitName}�� �й����� ���� �����մϴ�!");
            }
        }
    }
}