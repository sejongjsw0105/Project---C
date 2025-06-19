using System.Collections.Generic;
using UnityEngine;
public class Archer : Unit
{
    public ArcherSupport archerSupport;

    public Archer()
    {
        stats = new UnitStats 
        {
            maxHealth = 100,
            currentHealth = 100,
            attackPower = 20,
            defensePower = 5,
            range= 10
        };
        unitName = "Archer";
        unitId = 2;
        faction = Faction.Friendly;
        unitType = UnitType.Ranged;
        archerSupport = new ArcherSupport(this); // this = Archer ���� ���� �ѱ�
    }
    public override Support GetSupport()
    {
        return archerSupport; // ArcherSupport �ν��Ͻ��� ��ȯ
    }
    public class ArcherSupport : Support
    {
        public ArcherSupport(Archer owner)
            : base(
                  owner, // ������ �����ϴ� ����
                new List<AreaCondition> { AreaCondition.EnemyOccupied, AreaCondition.InCombat },
                new List<Faction> { Faction.Enemy }, // ���� ������ ������ ����
                owner.attackPower,
                owner.range
            )
        {
        }

    }
}
