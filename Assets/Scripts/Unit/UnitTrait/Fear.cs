using UnityEngine;
using System.Collections.Generic;

public class Fear : UnitTrait
{
    public Fear()
    {
        type = UnitTraitType.Fear;
        unitTypes = new List<Unit.UnitType> { Unit.UnitType.Melee, Unit.UnitType.Cavalry };

    }

    public override void OnAfterAttack(Unit attacker, Unit target, int damage)
    {
        if (target == null) return;

        // 체력이 1/4 이하일 경우 공포 발동
        if (target.health <= target.stats.maxHealth / 4 )
        {
            Debug.Log($"[Fear] {target.unitName}이 공포에 질려 도망칩니다!");
            target.Die();
        }
    }
}