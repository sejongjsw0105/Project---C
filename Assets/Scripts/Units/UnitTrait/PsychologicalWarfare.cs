using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PsychologicalWarfare : UnitTrait
{
    public PsychologicalWarfare()
    {
        traitName = "Psychological Warfare";
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry };
    }

    public override void OnAfterAttack(IUnit attacker, IUnit target, int damage)
    {
        if (target == null) return;

        if (target.currentHealth <= 0)
        {
            ReviveAsDefector(attacker, target);
        }
    }

    private void ReviveAsDefector(IUnit attacker, IUnit target)
    {

        // 체력 회복 (절반), 진영 변경
        target.currentHealth = target.stats.maxHealth / 2;
        target.faction = attacker.faction;

        // Defeated 상태 부여
        target.AddStatusEffect(new Defeated());

        if (target is Unit t)
        {
            // 유닛이 등록되어 있지 않다면 다시 등록
            if (!UnitManager.Instance.allUnits.Contains(t))
            {
                UnitManager.Instance.RegisterUnit(t);
            }
        }
        // 유닛이 소속된 지역에서 점유 정보도 업데이트
        if (target.area != null)
        {
            target.area.UpdateAreaCondition();
        }
    }
}
