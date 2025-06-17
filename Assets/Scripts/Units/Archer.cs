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

        archerSupport = new ArcherSupport(this); // this = Archer 유닛 참조 넘김
    }

    public class ArcherSupport : Support
    {
        public ArcherSupport(Archer owner)
            : base(
                  owner, // 지원을 제공하는 유닛
                new List<AreaCondition> { AreaCondition.EnemyOccupied, AreaCondition.InCombat },
                new List<Faction> { Faction.Enemy }, // 지원 가능한 진영은 적군
                0, // 일단 임시 값
                3
            )
        {
            // 여기서 공격력 비례 설정
            this.supportAmount = (int)(owner.attackPower * 1.5f);
        }
        public override void DoSupport(Area area)
        {
            BeforeSupport(area);
            if (area.occupyingEnemyUnit != null)
            {
                if(GridHelper.Instance.IsInRange(owner.area,area, supportRange))
                // 적 유닛이 있는 경우 지원 효과 적용
                area.occupyingEnemyUnit.Damaged(owner, DamageType.Support,supportAmount);
                Debug.Log($"{owner.unitName}가 {area.occupyingEnemyUnit.unitName}에게 지원을 제공했습니다. 지원량: {supportAmount}");
            }
            else
            {
                Debug.Log($"{owner.unitName}가 지원할 대상이 없습니다.");
            }
        }
    }
    public override Support GetSupport()
    {
        return archerSupport;
    }
}
