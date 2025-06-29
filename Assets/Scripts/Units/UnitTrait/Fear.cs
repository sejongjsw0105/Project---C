using UnityEngine;
using System.Collections.Generic;

public class Fear : UnitTrait
{
    public Fear()
    {
        traitName = "Fear";
        unitTypes = new List<UnitType> { UnitType.Melee, UnitType.Cavalry };

    }

    public override void OnAfterAttack(IUnit attacker, IUnit target, int damage)
    {
        if (target == null) return;

        // 체력이 1/4 이하일 경우 공포 발동
        if (target.currentHealth <= target.stats.maxHealth / 4 )
        {
            target.Die();
        }
    }
}