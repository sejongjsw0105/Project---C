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
        archerSupport = new ArcherSupport(this); // this = Archer 유닛 참조 넘김
    }
    public override Support GetSupport()
    {
        return archerSupport; // ArcherSupport 인스턴스를 반환
    }
    public class ArcherSupport : Support
    {
        public ArcherSupport(Archer owner)
            : base(
                  owner, // 지원을 제공하는 유닛
                new List<AreaCondition> { AreaCondition.EnemyOccupied, AreaCondition.InCombat },
                new List<Faction> { Faction.Enemy }, // 지원 가능한 진영은 적군
                owner.attackPower,
                owner.range
            )
        {
        }

    }
}
