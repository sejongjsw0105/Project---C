using System.Collections.Generic;

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

        if (target.faction == Faction.Friendly)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y - 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea); // ���� �̵�
            }
        }
        else if (target.faction == Faction.Enemy)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y + 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea);
            }
        }
    }
    public override void OnAfterSupport(Unit supporter, Area area, int value)
    {
        Unit target;
        if (supporter.faction == Faction.Friendly)
        {
            target = area.occupyingEnemyUnit; // ���� ����� �� ����
        }
        else if (supporter.faction == Faction.Enemy)
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

        if (target.faction == Faction.Friendly)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y - 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea); // ���� �̵�
            }
        }
        else if (target.faction == Faction.Enemy)
        {
            Area retreatArea = AreaManager.Instance.GetArea(x, y + 1);
            if (retreatArea != null)
            {
                target.DoMove(retreatArea);
            }
        }
    }
}