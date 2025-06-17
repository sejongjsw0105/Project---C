using System.Collections.Generic;
using UnityEngine;
public class Archer : Unit
{
    public ArcherSupport archerSupport;

    public Archer()
    {
        unitName = "Archer";
        unitId = 2;
        health = 80;
        attackPower = 20;
        defensePower = 5;
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;

        archerSupport = new ArcherSupport(this); // this = Archer ���� ���� �ѱ�
    }

    public class ArcherSupport : Support
    {
        public ArcherSupport(Archer owner)
            : base(
                  owner, // ������ �����ϴ� ����
                new List<AreaCondition> { AreaCondition.EnemyOccupied, AreaCondition.InCombat },
                new List<Faction> { Faction.Enemy }, // ���� ������ ������ ����
                0, // �ϴ� �ӽ� ��
                3
            )
        {
            // ���⼭ ���ݷ� ��� ����
            this.supportAmount = (int)(owner.attackPower * 1.5f);
        }
        public override void DoSupport(Area area)
        {
            BeforeSupport(area);
            if (area.occupyingEnemyUnit != null)
            {
                if(GridHelper.Instance.IsInRange(owner.area,area, supportRange))
                // �� ������ �ִ� ��� ���� ȿ�� ����
                area.occupyingEnemyUnit.Damaged(owner, DamageType.Support,supportAmount);
                Debug.Log($"{owner.unitName}�� {area.occupyingEnemyUnit.unitName}���� ������ �����߽��ϴ�. ������: {supportAmount}");
            }
            else
            {
                Debug.Log($"{owner.unitName}�� ������ ����� �����ϴ�.");
            }
        }
    }
    public override Support GetSupport()
    {
        return archerSupport;
    }
}
